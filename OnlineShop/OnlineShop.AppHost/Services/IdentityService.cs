using OnlineShop.AppHost.Extensions;

namespace OnlineShop.AppHost.Services;

public static class IdentityService
{
    public static void AddIdentityService(
        this IDistributedApplicationBuilder builder,
        IResourceBuilder<SqlServerDatabaseResource> database,
        IResourceBuilder<ParameterResource> jwtKey,
        IResourceBuilder<ParameterResource> jwtIssuer,
        IResourceBuilder<ParameterResource> jwtAudience,
        IResourceBuilder<ParameterResource> jwtExpire,
        IResourceBuilder<ParameterResource> jwtRefreshExpire)
    {
        builder.AddProject<Projects.Identity_API>("identity-api")
            .WithReference(database)
            .WithJwt(jwtKey, jwtIssuer, jwtAudience, jwtExpire, jwtRefreshExpire);
    }
}
