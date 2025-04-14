using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace KwestKarz.Services
{
    public static class TokenGenerator
    {
        /// <summary>
        /// Generates a JSON Web Token (JWT) string for the specified user identity and claims.
        /// </summary>
        /// <param name="userId">The unique identifier of the user (used as the subject claim).</param>
        /// <param name="email">The user's email address (used as the email claim).</param>
        /// <param name="claims">A collection of additional claims to include in the token.</param>
        /// <param name="secretKey">The symmetric secret key used to sign the token.</param>
        /// <param name="issuer">The issuer of the token (typically the issuing authority or application name).</param>
        /// <param name="audience">The intended audience of the token.</param>
        /// <param name="expiresIn">The duration until the token expires.</param>
        /// <returns>A signed JWT string containing the provided claims and identity information.</returns>
        public static string GenerateJwtToken(
            string userId,
            string email,
            IEnumerable<Claim> claims,
            string secretKey,
            string issuer,
            string audience,
            TimeSpan expiresIn)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var allClaims = new List<Claim>(claims)
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var expriration = DateTime.UtcNow.Add(expiresIn);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: allClaims,
                expires: expriration,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
