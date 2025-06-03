using KwestKarz.Entities;

namespace KwestKarz.Services
{
    public interface IVehicleService
    {
        Task<Vehicle> CreateAsync(Vehicle vehicle);
        Task DeleteAsync(Guid id);
        Task<List<Vehicle>> GetAllAsync();
        Task<Vehicle> GetByIdAsync(Guid id);
        Task<Vehicle> UpdateAsync(Vehicle vehicle);
    }
}
