using KwestKarz.Entities;
using Microsoft.EntityFrameworkCore;

namespace KwestKarz.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly KwestKarzDbContext _dbContext;

        public VehicleService(KwestKarzDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Vehicle>> GetAllAsync()
        {
            return await _dbContext.Vehicles.ToListAsync();
        }

        public async Task<Vehicle> GetByIdAsync(Guid id)
        {
            return await _dbContext.Vehicles.FindAsync(id);
        }

        public async Task<Vehicle> CreateAsync(Vehicle vehicle)
        {
            _dbContext.Vehicles.Add(vehicle);
            await _dbContext.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Vehicle> UpdateAsync(Vehicle vehicle)
        {
            _dbContext.Vehicles.Update(vehicle);
            await _dbContext.SaveChangesAsync();
            return vehicle;
        }

        public async Task DeleteAsync(Guid id)
        {
            var vehicle = await _dbContext.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _dbContext.Vehicles.Remove(vehicle);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
