using CsvHelper.Configuration;
using KwestKarz.Entities;

namespace KwestKarz.Services.CSV
{
    public class TripEarningsMap : ClassMap<TripEarnings>
    {
        public TripEarningsMap()
        {
            Map(m => m.ReservationID).Name("Reservation ID");
            Map(m => m.Guest).Name("Guest");
            Map(m => m.VehicleName).Name("Vehicle name");
            Map(m => m.Vehicle).Name("Vehicle");

            Map(m => m.TripStart)
                .Name("Trip start")
                .TypeConverter<UtcDateTimeConverter>();

            Map(m => m.TripEnd)
                .Name("Trip end")
                .TypeConverter<UtcDateTimeConverter>();

            Map(m => m.PickupLocation).Name("Pickup location");
            Map(m => m.ReturnLocation).Name("Return location");
            Map(m => m.TripStatus).Name("Trip status");
            Map(m => m.TotalEarnings).Name("Total earnings");
            Map(m => m.TotalEarnings)
                .Name("Total earnings")
                .TypeConverter<CurrencyConverter>();

            // Optional fallback
            Map(m => m.ImportedAt).Convert(_ => DateTime.UtcNow);
        }
    }
}
