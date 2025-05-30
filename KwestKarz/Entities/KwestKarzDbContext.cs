using KwestKarz.Entities;
using KwestKarz.Entities.Maintenance;
using Microsoft.EntityFrameworkCore;

namespace KwestKarz.Entities
{
    public class KwestKarzDbContext : DbContext
    {
        public KwestKarzDbContext(DbContextOptions<KwestKarzDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<TripEarnings> TripEarnings { get; set; }

        public DbSet<VehicleEvent> VehicleEvents { get; set; }

        // CRM Entities
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<OutstandingCharge> OutstandingCharges { get; set; }
        public DbSet<ContactLog> ContactLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables()
                .Build();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("KwestKarzDb"));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("kwestkarzbusinessdata");

            modelBuilder.Entity<VehicleEvent>()
                .HasDiscriminator<string>("EventType")
                .HasValue<MaintenanceEntry>("Maintenance")
                .HasValue<InspectionEntry>("Inspection")
                .HasValue<IncidentReport>("Incident")
                .HasValue<RepairEntry>("Repair");

            // Optional: Fluent configuration for CRM entities could go here later
        }
    }
}
