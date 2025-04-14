using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string PasswordHash { get; set; }

        public bool IsActive { get; set; }

        public bool RequiresPasswordReset { get; set; } = false;

        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}
