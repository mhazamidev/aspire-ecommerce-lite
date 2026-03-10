using Identity.Infrastructure.Persistence.Database;
using Identity.Infrastructure.Persistence.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Extensions;

public static class MigrationExtensions
{
    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await db.Database.MigrateAsync();
        await DbInitializer.SeedAsync(scope.ServiceProvider);
    }
}
