using Identity.Application.UOW;
using Identity.Domain.Users.Repositories;
using Identity.Domain.Users.Services;
using Identity.Infrastructure.Persistence.Implementation;
using Identity.Infrastructure.Persistence.Repositories;
using Identity.Infrastructure.Persistence.UOW;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.IoC.Services;

public static class SetupDI
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IIdentityUnitofWork, IdentityUnitofWork>();


        return services;
    }
}
