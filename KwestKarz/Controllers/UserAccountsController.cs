﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KwestKarz.Entities;
using KwestKarz.Services;

namespace KwestKarz.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public UserAccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<UserAccount>> GetAccountById(Guid id)
        {
            var user = await _accountService.GetAccountByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("{id}/disable")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DisableAccount(Guid id)
        {
            await _accountService.DisableAccountAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/enable")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EnableAccount(Guid id)
        {
            await _accountService.EnableAccountAsync(id);
            return NoContent();
        }




    }
}
