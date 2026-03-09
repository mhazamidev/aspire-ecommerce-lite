namespace OnlineShop.AppHost.Infrastructure.Databases;

public static class SqlServerConfig
{
    public static IResourceBuilder<SqlServerDatabaseResource> AddIdentityDatabase(
        this IDistributedApplicationBuilder builder,
        IResourceBuilder<ParameterResource> password)
    {

        return builder
            .AddSqlServer("sql", password: password)
            .WithDataVolume()
            .AddDatabase("sqlconnection", "onlineshop_identity");
    }
}
