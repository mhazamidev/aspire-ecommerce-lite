using Domain.SeekWork.Tracking;
using Identity.Domain.Users.Entities;
using Identity.Domain.Users.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Identity.Infrastructure.Persistence.Database.Context;

public class ApplicationDbContext : IdentityDbContext<IdentityUserEntity, IdentityRole<Guid>, Guid>
{

    public ApplicationDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
