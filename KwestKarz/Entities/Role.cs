using System.ComponentModel.DataAnnotations;

namespace KwestKarz.Entities
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<UserRole> Users { get; set; } = new List<UserRole>();
    }
}
