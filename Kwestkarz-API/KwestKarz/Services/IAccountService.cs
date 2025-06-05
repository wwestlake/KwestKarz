using KwestKarz.Entities;
using System;

namespace KwestKarz.Services
{
    public interface IAccountService
    {
        Task<UserAccount> CreateAccountAsync(string email, string username, string password);
        Task<List<UserAccount>> GetAllAccounts();
        Task DisableAccountAsync(Guid id);
        Task EnableAccountAsync(Guid id);
        Task<UserAccount> GetAccountByEmailAsync(string email);
        Task<UserAccount> GetAccountByIdAsync(Guid id);
        Task UpdateAccountAsync(UserAccount updated);
    }
}
