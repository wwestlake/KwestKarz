using System;
using System.ComponentModel.DataAnnotations;

namespace KwestKarz.Entities
{
    public class OutstandingCharge
    {
        [Key]
        public int ChargeId { get; set; }

        [Required]
        public int GuestId { get; set; }
        public int? TripId { get; set; }

        [Required]
        public string ChargeType { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Status { get; set; }

        public DateTime DateIncurred { get; set; }
        public DateTime? DateResolved { get; set; }

        public Guest Guest { get; set; }
    }
}
