using Identity.Domain.Users.Repositories;
using Identity.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.IoC.Services;

public static class SetupRepositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
