using KwestKarz.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KwestKarz.Services
{
    public class GuestService : IGuestService
    {
        private readonly KwestKarzDbContext _db;

        public GuestService(KwestKarzDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<Guest> GetGuestByIdAsync(int id)
        {
            return await _db.Guests
                .Include(g => g.Trips)
                .Include(g => g.ContactLogs)
                .Include(g => g.OutstandingCharges)
                .FirstOrDefaultAsync(g => g.GuestId == id);
        }

        public async Task<Guest> GetGuestByEmailAsync(string email)
        {
            return await _db.Guests
                .Include(g => g.Trips)
                .Include(g => g.ContactLogs)
                .Include(g => g.OutstandingCharges)
                .FirstOrDefaultAsync(g => g.Email == email);
        }

        public async Task<Guest> GetOrCreateGuestAsync(Guest guestInput)
        {
            var existing = await _db.Guests.FirstOrDefaultAsync(g => g.Email == guestInput.Email);
            if (existing != null) return existing;

            _db.Guests.Add(guestInput);
            await _db.SaveChangesAsync();
            return guestInput;
        }

        public async Task AddTripToGuestAsync(int guestId, Trip trip)
        {
            var guest = await _db.Guests.FindAsync(guestId);
            if (guest == null) throw new Exception("Guest not found");

            trip.GuestId = guestId;
            _db.Trips.Add(trip);
            await _db.SaveChangesAsync();
        }

        public async Task AddContactLogAsync(int guestId, ContactLog log)
        {
            var guest = await _db.Guests.FindAsync(guestId);
            if (guest == null) throw new Exception("Guest not found");

            log.GuestId = guestId;
            log.Timestamp = DateTime.UtcNow;
            _db.ContactLogs.Add(log);
            await _db.SaveChangesAsync();
        }

        public async Task AddOutstandingChargeAsync(int guestId, OutstandingCharge charge)
        {
            var guest = await _db.Guests.FindAsync(guestId);
            if (guest == null) throw new Exception("Guest not found");

            charge.GuestId = guestId;
            charge.DateIncurred = DateTime.UtcNow;
            _db.OutstandingCharges.Add(charge);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateGuestAsync(Guest updatedGuest)
        {
            var guest = await _db.Guests.FindAsync(updatedGuest.GuestId);
            if (guest == null) throw new Exception("Guest not found");

            guest.FullName = updatedGuest.FullName;
            guest.Email = updatedGuest.Email;
            guest.Phone = updatedGuest.Phone;
            guest.TuroUsername = updatedGuest.TuroUsername;
            guest.InternalRating = updatedGuest.InternalRating;
            guest.IsVIP = updatedGuest.IsVIP;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteGuestAsync(int guestId)
        {
            var guest = await _db.Guests.FindAsync(guestId);
            if (guest == null) return;

            _db.Guests.Remove(guest);
            await _db.SaveChangesAsync();
        }
    }
}
