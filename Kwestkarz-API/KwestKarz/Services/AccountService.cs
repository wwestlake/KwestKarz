using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KwestKarz.Entities;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace KwestKarz.Services
{
    public class AccountService : IAccountService
    {
        private readonly KwestKarzDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IPasswordHasher<UserAccount> _passwordHasher;
        private readonly string _baseUrl;

        public AccountService(
            KwestKarzDbContext dbContext,
            ITokenService tokenService,
            IEmailService emailService,
            IPasswordHasher<UserAccount> passwordHasher,
            IConfiguration config)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _emailService = emailService;
            _passwordHasher = passwordHasher;
            _baseUrl = config["BaseUrl"];
        }


        public async Task<List<UserAccount>> GetAllAccounts()
        {
            return await _dbContext.UserAccounts.ToListAsync();    
        }

        public async Task<UserAccount> CreateAccountAsync(string email, string username, string password)
        {
            var existing = await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Email == email);
            if (existing != null)
                throw new InvalidOperationException("Account already exists with this email.");

            var account = new UserAccount
            {
                Id = Guid.NewGuid(),
                Email = email,
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                IsActive = false // Locked until verified and approved
            };

            _dbContext.UserAccounts.Add(account);
            await _dbContext.SaveChangesAsync();
            return account;
        }

        public async Task InviteUserAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));

            var user = await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                user = new UserAccount
                {
                    Id = Guid.NewGuid(),
                    Email = email,
                    Username = email,
                    IsActive = true,
                    RequiresPasswordReset = true,
                    PasswordHash = string.Empty
                };
                _dbContext.UserAccounts.Add(user);
                await _dbContext.SaveChangesAsync();
            }

            var token = _tokenService.GenerateAccountSetupToken(user.Email);
            var setupLink = $"{_baseUrl}/setup-account?token={token}";

            await _emailService.SendEmailAsync(user.Email, "Complete Your Kwest Karz Account Setup",
                $"Click here to complete your setup: {setupLink}");
        }


        public async Task<UserAccount> GetAccountByIdAsync(Guid id)
        {
            return await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserAccount> GetAccountByEmailAsync(string email)
        {
            return await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task DisableAccountAsync(Guid id)
        {
            var user = await GetAccountByIdAsync(id);
            if (user == null) throw new Exception("User not found.");

            user.IsActive = false;
            await _dbContext.SaveChangesAsync();
        }

        public async Task EnableAccountAsync(Guid id)
        {
            var user = await GetAccountByIdAsync(id);
            if (user == null) throw new Exception("User not found.");

            user.IsActive = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(UserAccount updated)
        {
            var user = await GetAccountByIdAsync(updated.Id);
            if (user == null) throw new Exception("User not found.");

            user.Username = updated.Username;
            user.Email = updated.Email;
            await _dbContext.SaveChangesAsync();
        }

        public async Task CompleteAccountSetupAsync(string token, string password, string firstName, string lastName)
        {
            var principal = _tokenService.GetClaimsFromToken(token);
            if (principal == null)
                throw new SecurityTokenException("Invalid or expired token.");

            var purpose = principal.FindFirstValue("purpose");
            if (purpose != "account_setup")
                throw new SecurityTokenException("Invalid token purpose.");

            var email = principal.FindFirstValue(ClaimTypes.Email) ?? principal.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrWhiteSpace(email))
                throw new SecurityTokenException("Email not found in token.");

            var user = await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                throw new InvalidOperationException("User not found.");

            user.FirstName = firstName;
            user.LastName = lastName;
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            user.RequiresPasswordReset = false;

            await _dbContext.SaveChangesAsync();
        }


    }
}
