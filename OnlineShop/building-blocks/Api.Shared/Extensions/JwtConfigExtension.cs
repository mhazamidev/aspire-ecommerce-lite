using Identity.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Shared.Extensions;

public static class JwtConfigExtension
{
    public static IServiceCollection AddJwtConfig(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtOption>(option =>
        {
            option.Key = config.GetValue<string>("JwtKey") ?? string.Empty;
            option.Audience = config.GetValue<string>("JwtIssuer") ?? string.Empty;
            option.Issuer = config.GetValue<string>("JwtAudience") ?? string.Empty;
            option.TokenExpireInSecond = config.GetValue<int>("JwtTokenExpire");
            option.RefreshTokenExpireInSecond = config.GetValue<int>("JwtRefreshExpire");
        });

        return services;
    }
}
