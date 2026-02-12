using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.Json;
using OnlineShop.Identity.Entities;
using OnlineShop.Identity.Infrastructure;
using OnlineShop.Identity.Infrastructure.Behaviours;
using OnlineShop.Identity.Persistence;
using OnlineShop.Shared.Identity;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = false;
    options.SerializerOptions.PropertyNamingPolicy = null;//Pascalcase
    options.SerializerOptions.WriteIndented = true;
});


builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
           .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));


builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


builder.Services.AddCarter();

builder.Services.AddExceptionHandler<GlobalErrorHandler>();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("admin", policy => policy.RequireRole(RoleNames.Admin));


var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();

app.UseExceptionHandler(options => { });


using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
await dbContext.Database.MigrateAsync();
await dbContext.SeedAsync();


app.Run();
