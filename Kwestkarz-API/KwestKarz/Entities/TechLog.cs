using System;

namespace KwestKarz.Entities
{
    public enum TechLogLevel
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
        Exception,
        Other
    }

    public class TechLog
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public TechLogLevel Level { get; set; }
        public string Source { get; set; }  // filled by service, optionally overridden
        public string Message { get; set; }
        public string? Detail { get; set; }
    }
}
