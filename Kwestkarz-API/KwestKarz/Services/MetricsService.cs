using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KwestKarz.Entities;
using Microsoft.EntityFrameworkCore;

namespace KwestKarz.Services
{
    public class MetricsService : IMetricsService
    {
        private readonly KwestKarzDbContext _context;

        public MetricsService(KwestKarzDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetTotalEarningsAsync(DateTime start, DateTime end)
        {
            if (start > end)
                throw new ArgumentException("Start date must be before end date.");

            start = DateTime.SpecifyKind(start, DateTimeKind.Utc);
            end = DateTime.SpecifyKind(end, DateTimeKind.Utc);

            var total = await _context.TripEarnings
                .Where(te =>
                    te.TripStart.HasValue &&
                    te.TripEnd.HasValue &&
                    te.TripStart >= start &&
                    te.TripEnd <= end &&
                    te.TotalEarnings.HasValue)
                .SumAsync(te => te.TotalEarnings.Value);

            return Math.Round(total, 2);
        }

        public async Task<List<VehicleUtilizationResult>> GetUtilizationByVehicleAsync(DateTime start, DateTime end)
        {
            if (start > end)
                throw new ArgumentException("Start date must be before end date.");

            start = DateTime.SpecifyKind(start, DateTimeKind.Utc);
            end = DateTime.SpecifyKind(end, DateTimeKind.Utc);

            var totalDaysInRange = (end - start).TotalDays;

            var tripData = await _context.TripEarnings
                .Where(te =>
                    te.TripStart.HasValue &&
                    te.TripEnd.HasValue &&
                    te.TripStart >= start &&
                    te.TripEnd <= end)
                .AsNoTracking()
                .ToListAsync();

            var utilizationData = tripData
                .GroupBy(te => te.VehicleName)
                .Select(g => new VehicleUtilizationResult
                {
                    Vehicle = g.Key,
                    TotalDaysRented = (int)g.Sum(te =>
                        (te.TripEnd.Value - te.TripStart.Value).TotalDays),
                    TotalDaysInRange = totalDaysInRange,
                    UtilizationPercent = Math.Round(
                        100.0 * g.Sum(te =>
                            (te.TripEnd.Value - te.TripStart.Value).TotalDays) / totalDaysInRange, 2)
                })
                .ToList();

            return utilizationData;
        }
    }
}
