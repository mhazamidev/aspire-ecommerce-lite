using Api.Shared.Extensions;
using Carter;
using Identity.API.Extensions;
using Identity.Infrastructure.IoC.Services;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();
builder.Services
    .AddCarter()
    .AddVersion();

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
