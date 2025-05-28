using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KwestKarz.Entities;

namespace KwestKarz.Services
{
    public interface IVehicleEventService
    {
        Task<List<VehicleEvent>> GetEventsForVehicleAsync(Guid vehicleId);
        Task<VehicleEvent> GetEventByIdAsync(Guid id);
        Task<T> GetEventByIdAsync<T>(Guid id) where T : VehicleEvent;
        Task AddEventAsync(VehicleEvent vehicleEvent);
        Task DeleteEventAsync(Guid id);
    }
}
