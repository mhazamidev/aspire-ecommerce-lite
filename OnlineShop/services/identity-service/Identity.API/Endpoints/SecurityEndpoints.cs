using Api.Shared.Base;
using Carter;
using Identity.Application.User.Commands.Login;
using MediatR;

namespace Identity.API.Endpoints;

public class SecurityEndpoints : BaseModule, ICarterModule
{
    public SecurityEndpoints(ISender sender) : base(sender)
    {
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var mapGroup = app.MapGroup("/security").WithTags("Security");

        mapGroup.MapPost("/login", async (LoginCommand command) =>
        {
            return await Response(command);
        })
            .WithName("Login")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
    }
}
