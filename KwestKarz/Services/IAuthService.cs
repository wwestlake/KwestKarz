using System;
using System.Linq;

namespace KwestKarz.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string usernameOrEmail, string password);
        Task ChangePasswordAsync(string email, string currentPassword, string newPassword);

    }
}
