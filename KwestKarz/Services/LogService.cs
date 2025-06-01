using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using KwestKarz.Entities;
using Microsoft.EntityFrameworkCore;

namespace KwestKarz.Services
{
    public class LogService : ILogService
    {
        private readonly KwestKarzDbContext _db;

        public LogService(KwestKarzDbContext db)
        {
            _db = db;
        }

        public async Task LogAsync(
            TechLogLevel level,
            string message,
            string? detail = null,
            string? overrideSource = null,
            string callerFile = ""
        )
        {
            var source = !string.IsNullOrWhiteSpace(overrideSource)
                ? overrideSource
                : Path.GetFileNameWithoutExtension(callerFile);

            var log = new TechLog
            {
                Timestamp = DateTime.UtcNow,
                Level = level,
                Source = source,
                Message = message,
                Detail = detail
            };

            _db.TechLogs.Add(log);
            await _db.SaveChangesAsync();
        }

    }
}
