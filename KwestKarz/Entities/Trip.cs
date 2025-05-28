using System;
using System.ComponentModel.DataAnnotations;

namespace KwestKarz.Entities
{
    public class Trip
    {
        public int TripId { get; set; }

        [Required]
        public int GuestId { get; set; }

        [Required]
        public int VehicleId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Earnings { get; set; }
        public int MilesDriven { get; set; }

        public bool LateReturn { get; set; }
        public bool DamageReported { get; set; }
        public bool TollFlag { get; set; }

        public string Notes { get; set; }

        public Guest Guest { get; set; }
    }
}
