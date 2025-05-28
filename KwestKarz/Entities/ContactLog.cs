using System;

namespace KwestKarz.Entities
{
    public class ContactLog
    {
        public int LogId { get; set; }

        public int GuestId { get; set; }

        public string ContactType { get; set; }
        public DateTime Timestamp { get; set; }
        public string Notes { get; set; }

        public Guest Guest { get; set; }
    }
}
