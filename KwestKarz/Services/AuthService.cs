using Microsoft.EntityFrameworkCore;
using KwestKarz.Entities;
using System;
using System.Threading.Tasks;

namespace KwestKarz.Services
{
    /// <summary>
    /// Provides authentication services including user login and password changes.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly KwestKarzDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="tokenService">Service used to generate JWT tokens.</param>
        /// <param name="emailService">Service used to send email notifications.</param>
        public AuthService(
            KwestKarzDbContext dbContext,
            ITokenService tokenService,
            IEmailService emailService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        /// <summary>
        /// Attempts to authenticate a user using their email or username and password.
        /// Sends an email notification on success or failure (for audit/debugging purposes).
        /// </summary>
        /// <param name="usernameOrEmail">The user's email address or username.</param>
        /// <param name="password">The user's plaintext password to verify.</param>
        /// <returns>
        /// A signed JWT token string if authentication succeeds.
        /// Throws <see cref="UnauthorizedAccessException"/> if credentials are invalid or the account is inactive.
        /// </returns>
        public async Task<string> LoginAsync(string usernameOrEmail, string password)
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var user = await _dbContext.UserAccounts
                .Include(u => u.Roles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u =>
                    u.Email == usernameOrEmail ||
                    u.Username == usernameOrEmail);

            if (user == null || !user.IsActive)
            {
                await _emailService.SendEmailAsync(
                    "wwestlake@lagdaemon.com",
                    "Login Failed",
                    $"Login failed for: {usernameOrEmail} at {timestamp} (invalid or inactive)"
                );
                throw new UnauthorizedAccessException("Invalid credentials or inactive account.");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                await _emailService.SendEmailAsync(
                    "wwestlake@lagdaemon.com",
                    "Login Failed",
                    $"Login failed for: {usernameOrEmail} at {timestamp} (bad password)"
                );
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            await _emailService.SendEmailAsync(
                "wwestlake@lagdaemon.com",
                "Login Success",
                $"User {user.Email} logged in at {timestamp}"
            );

            return await _tokenService.GenerateTokenAsync(user.Email);
        }

        /// <summary>
        /// Changes the password for a user after verifying their current password.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="currentPassword">The current password to verify.</param>
        /// <param name="newPassword">The new password to set.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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
