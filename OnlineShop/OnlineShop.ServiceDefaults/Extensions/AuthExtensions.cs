using Identity.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OnlineShop.ServiceDefaults.Extensions;

public static class AuthExtensions
{
    public static IServiceCollection AddJwtAuth(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,

                ValidIssuer = config["jwtIssuer"],
                ValidAudience = config["jwtAudience"],

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(config["jwtKey"]!)
                )
            };
        });

        services.AddAuthorizationBuilder()
            .AddPolicy("admin", policy =>
                policy.RequireRole(RoleNames.Admin));

        return services;
    }
}