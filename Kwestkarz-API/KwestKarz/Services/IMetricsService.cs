using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KwestKarz.Services
{
    public interface IMetricsService
    {
        Task<decimal> GetTotalEarningsAsync(DateTime start, DateTime end);

        Task<List<VehicleUtilizationResult>> GetUtilizationByVehicleAsync(DateTime start, DateTime end);
    }

    public class VehicleUtilizationResult
    {
        public string Vehicle { get; set; }
        public int TotalDaysRented { get; set; }
        public double TotalDaysInRange { get; set; }
        public double UtilizationPercent { get; set; }
    }
}
