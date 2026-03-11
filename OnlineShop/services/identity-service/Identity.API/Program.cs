using Api.Shared.Extensions;
using Application.Shared.Extensions;
using Carter;
using Identity.API.Extensions;
using Identity.Application.Behaviours;
using Identity.Application.User.Commands.Login;
using Identity.Infrastructure.IoC.Services;
using MediatR;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();
builder.Services
    .AddCarter()
    .AddVersion();

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));

builder.Services.AddMediatRConfig(typeof(LoginCommand).Assembly);

builder.Services.AddRepositories();

builder.Services.AddJwtConfig(builder.Configuration);


var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapVersionedGroup()
    .MapCarter();

app.UseAuthentication();
app.UseAuthorization();

await app.ApplyMigrationsAsync();

app.Run();
