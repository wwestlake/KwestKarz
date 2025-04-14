using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using KwestKarz.Entities;

namespace KwestKarz.Services
{
    public class TokenService : ITokenService
    {
        private readonly KwestKarzDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public TokenService(KwestKarzDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<string> GenerateTokenAsync(string email)
        {
            var user = await _dbContext.UserAccounts
                .Include(u => u.Roles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new Exception("User not found");


            var claims = user.Roles
                .Select(ur => new Claim(ClaimTypes.Role, ur.Role.Name))
                .ToList();

            if (user.RequiresPasswordReset)
            {
                claims.Add(new Claim("requiresPasswordReset", "true"));
            }


            var secretKey = _configuration["Jwt:SecretKey"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expiresIn = TimeSpan.FromHours(1);

            return TokenGenerator.GenerateJwtToken(
                user.Id.ToString(),
                user.Email,
                claims,
                secretKey,
                issuer,
                audience,
                expiresIn);
        }
    }
}
