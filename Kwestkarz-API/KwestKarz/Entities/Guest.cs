using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KwestKarz.Entities
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string TuroUsername { get; set; }

        [Range(1, 5)]
        public int? InternalRating { get; set; }

        public bool IsVIP { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        public ICollection<Trip> Trips { get; set; }
        public ICollection<OutstandingCharge> OutstandingCharges { get; set; }
        public ICollection<ContactLog> ContactLogs { get; set; }
    }
}
