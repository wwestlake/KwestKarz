using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using KwestKarz.Entities;

namespace KwestKarz.Services
{
    public class TripEarningsService : ITripEarningsService
    {
        private readonly KwestKarzDbContext _dbContext;
        private readonly ICSVParserService _csvParserService;

        public TripEarningsService(KwestKarzDbContext dbContext, ICSVParserService csvParserService)
        {
            _dbContext = dbContext;
            _csvParserService = csvParserService;
        }

        public int ImportTripEarnings(Stream csvStream)
        {
            var records = _csvParserService.ParseCsv<TripEarnings>(csvStream);

            foreach (var record in records)
            {
                var raw = JsonSerializer.Serialize(record);
                using var sha256 = SHA256.Create();
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(raw));
                record.RowHash = Convert.ToHexString(hashBytes);
            }

            var newHashes = records.Select(r => r.RowHash).ToHashSet();

            var existingHashes = _dbContext.TripEarnings
                .Where(te => newHashes.Contains(te.RowHash))
                .Select(te => te.RowHash)
                .ToHashSet();

            var uniqueRecords = records
                .Where(r => !existingHashes.Contains(r.RowHash))
                .ToList();

            if (uniqueRecords.Any())
            {
                _dbContext.TripEarnings.AddRange(uniqueRecords);
                _dbContext.SaveChanges();
            }

            return uniqueRecords.Count;
        }

        private string ComputeHash(TripEarnings entry)
        {
            using var sha = SHA256.Create();
            var input = $"{entry.ReservationID}|{entry.TripStart:o}|{entry.Vehicle}|{entry.TotalEarnings}";
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }
}
