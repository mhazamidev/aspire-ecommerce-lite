using OnlineShop.AppHost.Extensions;

namespace OnlineShop.AppHost.Services;

public static class ProductService
{
    public static void AddProductService(
        this IDistributedApplicationBuilder builder,
        IResourceBuilder<ParameterResource> jwtKey,
        IResourceBuilder<ParameterResource> jwtIssuer,
        IResourceBuilder<ParameterResource> jwtAudience)
    {
        builder.AddProject<Projects.Product_API>("product-api")
            .WithJwt(jwtKey, jwtIssuer, jwtAudience);
    }
}
