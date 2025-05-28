using System;

namespace KwestKarz.Services
{
    public interface IVehicleImportService
    {
        Task<int> ImportVehiclesAsync(Stream csvStream);
    }
}
