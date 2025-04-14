using Microsoft.EntityFrameworkCore;
using KwestKarz.Entities;

namespace KwestKarz.Services
{
    public class AuthService : IAuthService
    {
        private readonly KwestKarzDbContext _dbContext;
        private readonly ITokenService _tokenService;

        public AuthService(KwestKarzDbContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Attempts to authenticate a user using their email or username and password.
        /// </summary>
        /// <param name="usernameOrEmail">The user's email address or username.</param>
        /// <param name="password">The user's plaintext password to verify.</param>
        /// <returns>
        /// A signed JWT token string if authentication succeeds.
        /// Throws <see cref="UnauthorizedAccessException"/> if credentials are invalid or the account is inactive.
        /// </returns>
        public async Task<string> LoginAsync(string usernameOrEmail, string password)
        {
            var user = await _dbContext.UserAccounts
                .Include(u => u.Roles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u =>
                    u.Email == usernameOrEmail ||
                    u.Username == usernameOrEmail);

            if (user == null || !user.IsActive)
                throw new UnauthorizedAccessException("Invalid credentials or inactive account.");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials.");

            return await _tokenService.GenerateTokenAsync(user.Email);
        }


        public async Task ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            var user = await _dbContext.UserAccounts
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !user.IsActive)
                throw new UnauthorizedAccessException("Invalid user or inactive account.");

            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                throw new UnauthorizedAccessException("Current password is incorrect.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.RequiresPasswordReset = false;

            await _dbContext.SaveChangesAsync();
        }
    }
}
