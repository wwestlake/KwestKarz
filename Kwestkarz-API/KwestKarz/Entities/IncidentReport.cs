using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KwestKarz.Entities;

namespace KwestKarz.Entities.Maintenance
{
    public class IncidentReport : VehicleEvent
    {
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string Severity { get; set; } // e.g., "Minor", "Major", "Critical"

        public bool ReportedToTuro { get; set; }

        [MaxLength(100)]
        public string ClaimId { get; set; }
    }
}
