using KwestKarz.Entities;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KwestKarz.Services
{
    public class VehicleImportService : IVehicleImportService
    {
        private readonly KwestKarzDbContext _dbContext;
        private readonly ICSVParserService _csvParser;

        public VehicleImportService(KwestKarzDbContext dbContext, ICSVParserService csvParser)
        {
            _dbContext = dbContext;
            _csvParser = csvParser;
        }

        public async Task<int> ImportVehiclesAsync(Stream csvStream)
        {
            var records = _csvParser.ParseCsv<Vehicle>(csvStream);

            // Normalize VINs and remove duplicates against database
            var existingVins = await _dbContext.Vehicles
                .Select(v => v.VIN.ToUpper())
                .ToListAsync();

            var newVehicles = records
                .Where(v => !string.IsNullOrWhiteSpace(v.VIN))
                .Where(v => !existingVins.Contains(v.VIN.Trim().ToUpper()))
                .ToList();

            foreach (var vehicle in newVehicles)
            {
                vehicle.Id = Guid.NewGuid();

                if (vehicle.PurchaseDate.HasValue && vehicle.PurchaseDate.Value.Kind == DateTimeKind.Unspecified)
                {
                    vehicle.PurchaseDate = DateTime.SpecifyKind(vehicle.PurchaseDate.Value, DateTimeKind.Utc);
                }
            }

            _dbContext.Vehicles.AddRange(newVehicles);
            await _dbContext.SaveChangesAsync();

            return newVehicles.Count;
        }
    }
}
