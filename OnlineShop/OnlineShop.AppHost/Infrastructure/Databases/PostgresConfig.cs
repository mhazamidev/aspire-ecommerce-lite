namespace OnlineShop.AppHost.Infrastructure.Databases;

public static class PostgresConfig
{
    public static IResourceBuilder<PostgresDatabaseResource> AddCatalogDatabase(
       this IDistributedApplicationBuilder builder,
       IResourceBuilder<ParameterResource> username,
       IResourceBuilder<ParameterResource> password)
    {
        var postgres = builder
            .AddPostgres("postgres", username, password)
            .WithDataVolume();

        return postgres.AddDatabase("postgresconnection", "onlineshop_catalog");
    }
}
