using Application.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;
using Application.Shared.Behaviours;

namespace Application.Shared.Extensions;

public static class MediatRExtensions
{
    public static void AddMediatR(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        })
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddValidatorsFromAssembly(assembly);
    }
}
