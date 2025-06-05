using System;
using System.Linq;
using System.Security.Claims;

namespace KwestKarz.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(string email);
        string GenerateAccountSetupToken(string email);
        ClaimsPrincipal GetClaimsFromToken(string token);
    }
}
