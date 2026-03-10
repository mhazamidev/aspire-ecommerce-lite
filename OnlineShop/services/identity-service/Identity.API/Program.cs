using Api.Shared.Extensions;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();
builder.Services
    .AddCarter()
    .AddVersion();


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

app.Run();
