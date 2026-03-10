using Identity.Domain.Users.Entities;
using Identity.Infrastructure.Persistence.Database.Context;
using Identity.Infrastructure.Persistence.Tracking.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.IoC.Services;

public static class SetupDbContext
{
    public static IServiceCollection AddIdentityDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>((provider, options) =>
        {
            var interceptor = provider.GetRequiredService<AuditSaveChangesInterceptor>();
            options.UseSqlServer(connectionString)
            .AddInterceptors(interceptor);
        });

        services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

        services.AddSingleton<AuditSaveChangesInterceptor>();

        return services;
    }
}
