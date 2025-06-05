using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using KwestKarz.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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

        public string GenerateAccountSetupToken(string email)
        {
            var expiryHours = _configuration.GetValue<int>("AccountSetup:TokenExpiryHours");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim("purpose", "account_setup")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(expiryHours);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal GetClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(5)
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

    }
}
