using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KwestKarz.Services;
using System;
using System.Threading.Tasks;

namespace KwestKarz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAccountService _accountService;

        public AuthController(IAuthService authService, IAccountService accountService)
        {
            _authService = authService;
            _accountService = accountService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                // TEMPORARY: this gives you insight into what blew up
                return StatusCode(500, $"Login failed: {ex.Message}");
            }
        }

        [HttpPost("create-account")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount([FromBody] CreateUserRequest request)
        {
            try
            {
                var user = await _accountService.CreateAccountAsync(request.Email, request.Username, request.Password);
                return CreatedAtAction(nameof(UserAccountsController.GetAccountById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized("Email claim not found in token.");

            try
            {
                await _authService.ChangePasswordAsync(email, request.CurrentPassword, request.NewPassword);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
