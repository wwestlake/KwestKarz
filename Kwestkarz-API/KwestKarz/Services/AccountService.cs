using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KwestKarz.Entities;
using BCrypt.Net;

namespace KwestKarz.Services
{
    public class AccountService : IAccountService
    {
        private readonly KwestKarzDbContext _dbContext;

        public AccountService(KwestKarzDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
