using Identity.Domain.Users.Entities;
using Identity.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Persistence.Database;

public class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
        var userManager = services.GetRequiredService<UserManager<User>>();

        if (!await roleManager.RoleExistsAsync(RoleNames.Admin))
        {
            await roleManager.CreateAsync(new IdentityRole<int>(RoleNames.Admin));
        }

        var adminUser = await userManager.FindByNameAsync("admin");

        if (adminUser == null)
        {
            adminUser = User.Create("Admin", "Admin", "Admin", "support@payportnow.com", true);

            await userManager.CreateAsync(adminUser, "Admin123456");
            await userManager.AddToRoleAsync(adminUser, RoleNames.Admin);
        }
    }
}
