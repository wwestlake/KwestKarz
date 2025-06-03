using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KwestKarz.Entities;

namespace KwestKarz.Entities.Maintenance
{
    public class InspectionEntry : VehicleEvent
    {
        [Required]
        [MaxLength(100)]
        public string Inspector { get; set; }

        [Required]
        [MaxLength(20)]
        public string Result { get; set; } // e.g., "Pass", "Fail"

        [MaxLength(100)]
        public string InspectionType { get; set; } // e.g., "State Safety", "Pre-Trip", "Annual"
    }
}
