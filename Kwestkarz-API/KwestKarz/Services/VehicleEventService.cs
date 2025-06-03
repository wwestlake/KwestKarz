using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KwestKarz.Entities;
using Microsoft.EntityFrameworkCore;

namespace KwestKarz.Services
{
    public class VehicleEventService : IVehicleEventService
    {
        private readonly KwestKarzDbContext _dbContext;

        public VehicleEventService(KwestKarzDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<VehicleEvent>> GetEventsForVehicleAsync(Guid vehicleId)
        {
            return await _dbContext.VehicleEvents
                .Where(e => e.VehicleId == vehicleId)
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }

        public async Task<VehicleEvent> GetEventByIdAsync(Guid id)
        {
            return await _dbContext.VehicleEvents.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> GetEventByIdAsync<T>(Guid id) where T : VehicleEvent
        {
            return await _dbContext.VehicleEvents
                .OfType<T>()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddEventAsync(VehicleEvent vehicleEvent)
        {
            _dbContext.VehicleEvents.Add(vehicleEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(Guid id)
        {
            var ev = await _dbContext.VehicleEvents.FirstOrDefaultAsync(e => e.Id == id);
            if (ev != null)
            {
                _dbContext.VehicleEvents.Remove(ev);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
