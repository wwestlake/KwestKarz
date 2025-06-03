using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KwestKarz.Entities;
using KwestKarz.Services;
using Microsoft.AspNetCore.Mvc;

namespace KwestKarz.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/apikeys")]
    public class ApiKeyAdminController : ControllerBase
    {
        private readonly IApiKeyService _apiKeyService;

        public ApiKeyAdminController(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateApiKey([FromBody] CreateApiKeyRequest request)
        {
            var key = await _apiKeyService.CreateKeyAsync(request.Name, request.Description, request.Roles);
            return Ok(new { key.Id, ApiKey = key.KeyHash }); // Return raw key
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var keys = await _apiKeyService.GetAllKeysAsync();
            return Ok(keys.Select(k => new
            {
                k.Id,
                k.Name,
                k.Description,
                k.DateIssued,
                k.Expires,
                k.IsActive,
                Claims = k.Claims.Select(c => c.Role.ToString())
            }));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var success = await _apiKeyService.DeactivateKeyAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("{id:guid}/claims")]
        public async Task<IActionResult> GetClaims(Guid id)
        {
            var key = await _apiKeyService.GetKeyByIdAsync(id);
            if (key == null) return NotFound();

            return Ok(key.Claims.Select(c => c.Role.ToString()));
        }
    }

    public class CreateApiKeyRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<ApiClientRole> Roles { get; set; } = new();
    }
}
