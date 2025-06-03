using KwestKarz.Entities;

namespace KwestKarz.Services
{
    public static class DbSeeder
    {
        public static void SeedRoles(KwestKarzDbContext dbContext)
        {
            var requiredRoles = new[] { "Admin", "Employee", "Maintenance", "Owner", "Driver" };

            foreach (var roleName in requiredRoles)
            {
                if (!dbContext.Roles.Any(r => r.Name == roleName))
                {
                    dbContext.Roles.Add(new Role { Id = Guid.NewGuid(), Name = roleName });
                }
            }

            dbContext.SaveChanges();
        }


        public static void SeedAdminUser(KwestKarzDbContext dbContext)
        {
            var adminEmail = "admin@kwestkarz.local";

            if (!dbContext.UserAccounts.Any(u => u.Email == adminEmail))
            {
                var adminRole = dbContext.Roles.First(r => r.Name == "Admin");

                var user = new UserAccount
                {
                    Id = Guid.NewGuid(),
                    Email = adminEmail,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
                    IsActive = true,
                    RequiresPasswordReset = true,
                    Roles = new List<UserRole>
                {
                    new UserRole
                    {
                        Id = Guid.NewGuid(),
                        RoleId = adminRole.Id
                    }
                }
                };

                dbContext.UserAccounts.Add(user);
                dbContext.SaveChanges();
            }
        }

    }
}
