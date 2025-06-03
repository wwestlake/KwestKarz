using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KwestKarz.Entities;

namespace KwestKarz.Entities.Maintenance
{
    public class MaintenanceEntry : VehicleEvent
    {
        [Required]
        [MaxLength(100)]
        public string ServiceType { get; set; } // e.g., "Oil Change", "Brake Pads", "Battery"

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }

        [MaxLength(100)]
        public string PerformedBy { get; set; } // Vendor, technician, or internal
    }
}
