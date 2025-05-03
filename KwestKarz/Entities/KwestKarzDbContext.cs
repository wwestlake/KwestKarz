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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships and constraints here
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("kwestkarzbusinessdata");
        }
    } 
}
