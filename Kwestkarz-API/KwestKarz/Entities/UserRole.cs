using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KwestKarz.Entities
{
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
