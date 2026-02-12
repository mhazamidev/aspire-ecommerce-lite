using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OnlineShop.Identity.Entities;
using System.Reflection;

namespace OnlineShop.Identity.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    :IdentityDbContext<User,IdentityRole<int>,int>(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
}
