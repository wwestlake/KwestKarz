using System;
using System.Linq;

namespace KwestKarz.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(string email);
    }
}
