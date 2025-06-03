using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using KwestKarz.Entities;
using Microsoft.EntityFrameworkCore;

namespace KwestKarz.Services
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly KwestKarzDbContext _db;

        public ApiKeyService(KwestKarzDbContext db)
        {
            _db = db;
        }

        public async Task<ApiClientKey> CreateKeyAsync(string name, string? description, IEnumerable<ApiClientRole> roles)
        {
            var rawKey = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N"); // 64-char token
            var hash = Hash(rawKey);

            var key = new ApiClientKey
            {
                Id = Guid.NewGuid(),
                Name = name,
                KeyHash = hash,
                Description = description,
                DateIssued = DateTime.UtcNow,
                IsActive = true,
                Claims = roles.Select(r => new ApiClientClaim { Role = r }).ToList()
            };

            _db.ApiClientKeys.Add(key);
            await _db.SaveChangesAsync();

            key.KeyHash = rawKey; // Return raw key in place of hash (not persisted)
            return key;
        }

        public async Task<List<ApiClientKey>> GetAllKeysAsync()
        {
            return await _db.ApiClientKeys
                .Include(k => k.Claims)
                .ToListAsync();
        }

        public async Task<ApiClientKey?> GetKeyByIdAsync(Guid id)
        {
            return await _db.ApiClientKeys
                .Include(k => k.Claims)
                .FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<bool> DeactivateKeyAsync(Guid id)
        {
            var key = await _db.ApiClientKeys.FindAsync(id);
            if (key == null) return false;

            key.IsActive = false;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ApiClientKey?> ValidateKeyAsync(string rawKey)
        {
            var hash = Hash(rawKey);
            return await _db.ApiClientKeys
                .Include(k => k.Claims)
                .FirstOrDefaultAsync(k => k.KeyHash == hash && k.IsActive);
        }

        private static string Hash(string key)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(key);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
