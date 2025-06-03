using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KwestKarz.Entities;

namespace KwestKarz.Entities.Maintenance
{
    public class RepairEntry : VehicleEvent
    {
        [Required]
        [MaxLength(100)]
        public string ComponentAffected { get; set; }

        [MaxLength(100)]
        public string RepairType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }

        [MaxLength(100)]
        public string ShopName { get; set; }
    }
}
