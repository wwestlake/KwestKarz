using System;
using System.Collections.Generic;

namespace KwestKarz.Entities
{
    public class ApiClientKey
    {
        public Guid Id { get; set; }
        public string Name { get; set; }                     // Application name
        public string KeyHash { get; set; }                  // Store hashed key
        public string? Description { get; set; }
        public DateTime DateIssued { get; set; } = DateTime.UtcNow;
        public DateTime? Expires { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastUsed { get; set; }

        public List<ApiClientClaim> Claims { get; set; } = new();
    }
}
