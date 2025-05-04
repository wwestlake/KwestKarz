using CsvHelper.Configuration.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace KwestKarz.Entities
{
    public class TripEarnings
    {
        [Key]
        [Name("Reservation ID")]
        public string ReservationID { get; set; }

        public string? Guest { get; set; }
        public string? Vehicle { get; set; }

        [Name("Vehicle name")]
        public string? VehicleName { get; set; }
        public DateTime? TripStart { get; set; }
        public DateTime? TripEnd { get; set; }
        public string? PickupLocation { get; set; }
        public string? ReturnLocation { get; set; }
        public string? TripStatus { get; set; }
        public decimal? TotalEarnings { get; set; }

        public DateTime ImportedAt { get; set; }
        public string RowHash { get; set; }
    }
}
