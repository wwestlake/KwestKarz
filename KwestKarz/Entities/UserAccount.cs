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

        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
    }

    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<UserRole> Users { get; set; } = new List<UserRole>();
    }

    public class UserRole
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("UserAccount")]
        public Guid UserAccountId { get; set; }

        public UserAccount UserAccount { get; set; }

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }

        public Role Role { get; set; }
    }
}
