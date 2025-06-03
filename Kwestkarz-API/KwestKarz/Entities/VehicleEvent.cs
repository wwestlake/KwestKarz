using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KwestKarz.Entities
{
    public abstract class VehicleEvent
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Vehicle")]
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int? Odometer { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; } // optional: user, admin, automation
    }
}
