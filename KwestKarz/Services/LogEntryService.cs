using System;
using System.Collections.Generic;
using System.Linq;
using KwestKarz.Entities;

namespace KwestKarz.Services
{
    public class LogEntryService
    {
        private readonly List<LogEntry> _logStore;

        public LogEntryService()
        {
            _logStore = new List<LogEntry>();
        }

        public LogEntry CreateLog(string account, string category, string action, string result, string type)
        {
            var previous = _logStore.LastOrDefault();

            var log = new LogEntry
            {
                Account = account,
                Category = category,
                Action = action,
                Result = result,
                Type = type,
                Timestamp = DateTime.UtcNow
            };

            log.ComputeHash(previous?.Hash);
            _logStore.Add(log);
            return log;
        }

        public IEnumerable<LogEntry> GetLogsForAccount(string account)
        {
            return _logStore.Where(log => log.Account == account).OrderBy(log => log.Timestamp);
        }

        public TimeSpan CalculateTimeOnTask(string account, string category)
        {
            var logs = GetLogsForAccount(account).Where(l => l.Category == category).ToList();
            if (logs.Count < 2) return TimeSpan.Zero;

            // Pair start/stop events or estimate from earliest to latest
            var first = logs.First().Timestamp;
            var last = logs.Last().Timestamp;
            return last - first;
        }

        public bool ValidateChain()
        {
            string lastHash = null;
            foreach (var entry in _logStore)
            {
                if (!entry.ValidateHash() || entry.PreviousHash != lastHash)
                    return false;
                lastHash = entry.Hash;
            }
            return true;
        }
    }
}
