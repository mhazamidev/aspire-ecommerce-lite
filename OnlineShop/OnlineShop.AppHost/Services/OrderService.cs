using OnlineShop.AppHost.Extensions;

namespace OnlineShop.AppHost.Services;

public static class OrderService
{
    public static void AddOrderService(this IDistributedApplicationBuilder builder,
        IResourceBuilder<ParameterResource> jwtKey,
        IResourceBuilder<ParameterResource> jwtIssuer,
        IResourceBuilder<ParameterResource> jwtAudience)
    {
        builder.AddProject<Projects.Order_API>("order-api")
            .WithJwt(jwtKey, jwtIssuer, jwtAudience);
    }
}
