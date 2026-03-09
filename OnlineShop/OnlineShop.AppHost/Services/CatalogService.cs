using OnlineShop.AppHost.Extensions;

namespace OnlineShop.AppHost.Services;

public static class CatalogService
{
    public static void AddCatalogService(
       this IDistributedApplicationBuilder builder,
       IResourceBuilder<PostgresDatabaseResource> database,
       IResourceBuilder<ParameterResource> jwtKey,
       IResourceBuilder<ParameterResource> jwtIssuer,
       IResourceBuilder<ParameterResource> jwtAudience)
    {
        builder.AddProject<Projects.Catalog_API>("catalog-api")
            .WithReference(database)
            .WithJwt(jwtKey, jwtIssuer, jwtAudience);
    }
}
