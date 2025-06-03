using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KwestKarz.Entities;

namespace KwestKarz.Services
{
    public interface IApiKeyService
    {
        Task<ApiClientKey> CreateKeyAsync(string name, string? description, IEnumerable<ApiClientRole> roles);
        Task<List<ApiClientKey>> GetAllKeysAsync();
        Task<ApiClientKey?> GetKeyByIdAsync(Guid id);
        Task<bool> DeactivateKeyAsync(Guid id);
        Task<ApiClientKey?> ValidateKeyAsync(string rawKey);
    }
}
