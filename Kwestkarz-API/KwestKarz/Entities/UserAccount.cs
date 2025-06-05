using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KwestKarz.Entities
{
    public class UserAccount
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        [JsonIgnore]
        public string PasswordHash { get; set; }

        public bool IsActive { get; set; }

        public bool RequiresPasswordReset { get; set; } = false;

        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();

        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }
    }
}
