using KwestKarz.Entities;
using System;
using System.Linq;

namespace KwestKarz.Services
{
    public interface IGuestService
    {
        Task AddContactLogAsync(int guestId, ContactLog log);
        Task AddOutstandingChargeAsync(int guestId, OutstandingCharge charge);
        Task AddTripToGuestAsync(int guestId, Trip trip);
        Task DeleteGuestAsync(int guestId);
        Task<Guest> GetGuestByEmailAsync(string email);
        Task<Guest> GetGuestByIdAsync(int id);
        Task<Guest> GetOrCreateGuestAsync(Guest guestInput);
        Task UpdateGuestAsync(Guest updatedGuest);
    }
}
