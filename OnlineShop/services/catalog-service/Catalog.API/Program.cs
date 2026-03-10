using Api.Shared.Extensions;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();


builder.Services.AddOpenApi();
builder.Services
    .AddCarter()
    .AddVersion();
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

app.Run();
