using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KwestKarz.Entities
{
    public class Vehicle
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Make { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        [Required]
        [Range(1886, 2100)] // The first car was invented in 1886 :)
        public int Year { get; set; }

        [Required]
        [MaxLength(100)]
        public string Color { get; set; }

        [MaxLength(50)]
        public string PaintCode { get; set; }

        [MaxLength(100)]
        public string Package { get; set; }

        [Required]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "VIN must be exactly 17 characters.")]
        public string VIN { get; set; }

        [Required]
        [MaxLength(20)]
        public string LicensePlateNumber { get; set; }

    }
}
