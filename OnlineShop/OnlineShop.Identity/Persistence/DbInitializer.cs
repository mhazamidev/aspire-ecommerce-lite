using OnlineShop.Identity.Entities;
using OnlineShop.Shared.Identity;

namespace OnlineShop.Identity.Persistence
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(this AppDbContext dbContext)
        {
            await dbContext.Database.EnsureCreatedAsync();

            if (await dbContext.Roles.AnyAsync())
                return;


            using var transaction = await dbContext.Database.BeginTransactionAsync();

            var adminRole = new IdentityRole<int>
            {
                Name = RoleNames.Admin,
                NormalizedName = RoleNames.Admin.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var cutomerRole = new IdentityRole<int>
            {
                Name = RoleNames.Customer,
                NormalizedName = RoleNames.Customer.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            dbContext.Roles.AddRange(adminRole,cutomerRole);
            await dbContext.SaveChangesAsync();

            User adminUser = new()
            {
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "admin",
                NormalizedUserName = "admin".Normalize().ToUpperInvariant(),
                FirstName = "John",
                LastName = "Duo",
                PasswordHash = "AQAAAAIAAYagAAAAELVm1dzFfGRmHoH3TtqbteW0FTmf5WYn01k43mSFD+2GK119v9+GkP12BZ4x6mFNlA=="
            };
            //Admin123456

            dbContext.Users.Add(adminUser);
            await dbContext.SaveChangesAsync();


            dbContext.UserRoles.Add(new IdentityUserRole<int>
            {
                UserId = adminUser.Id,
                RoleId = adminRole.Id
            });

            await dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
    }
}
