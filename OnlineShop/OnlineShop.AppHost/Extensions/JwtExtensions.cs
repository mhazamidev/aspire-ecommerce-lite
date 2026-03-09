using OnlineShop.AppHost.Constants;

namespace OnlineShop.AppHost.Extensions;

internal static class JwtExtensions
{
    public static IResourceBuilder<ProjectResource> WithJwt(this IResourceBuilder<ProjectResource> builder,
        IResourceBuilder<ParameterResource> key,
        IResourceBuilder<ParameterResource> issuer,
        IResourceBuilder<ParameterResource> audience,
        IResourceBuilder<ParameterResource>? expire = null,
        IResourceBuilder<ParameterResource>? refreshExpire = null)
    {

        builder = builder
         .WithEnvironment(Env.JwtKey, key)
         .WithEnvironment(Env.JwtIssuer, issuer)
         .WithEnvironment(Env.JwtAudience, audience);

        if (expire is not null)
            builder = builder.WithEnvironment(Env.JwtTokenExpire, expire);

        if (refreshExpire is not null)
            builder = builder.WithEnvironment(Env.JwtRefreshExpire, refreshExpire);

        return builder;
    }
}
