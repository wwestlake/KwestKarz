using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace KwestKarz.Entities
{
    public class LogEntry
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Action { get; set; }
        public string Result { get; set; }
        public string Account { get; set; } // email or userId
        public string Category { get; set; } // e.g., "Auth", "Procedure", "Task"
        public string Type { get; set; } // e.g., "Success", "Failure"

        public string PreviousHash { get; set; }
        public string Hash { get; private set; }

        public void ComputeHash(string previousHash)
        {
            PreviousHash = previousHash;
            Hash = GenerateHash(previousHash);
        }

        public bool ValidateHash()
        {
            var expectedHash = GenerateHash(PreviousHash);
            return Hash == expectedHash;
        }

        private string GenerateHash(string previousHash)
        {
            var payload = new
            {
                Id,
                Timestamp,
                Action,
                Result,
                Account,
                Category,
                Type,
                PreviousHash = previousHash
            };

            var json = JsonConvert.SerializeObject(payload);
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(json);
                var hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToHexString(hashBytes);
            }
        }
    }
}
