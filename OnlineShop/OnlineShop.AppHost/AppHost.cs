using OnlineShop.AppHost.Infrastructure.Databases;
using OnlineShop.AppHost.Services;

var builder = DistributedApplication.CreateBuilder(args);

#region parameters

var sqlPassword = builder.AddParameter("SqlPassword", secret: true);  

var jwtKey = builder.AddParameter("JwtKey", secret: true);
var jwtIssuer = builder.AddParameter("JwtIssuer", secret: true);
var jwtAudience = builder.AddParameter("JwtAudience", secret: true);
var jwtTokenExpire = builder.AddParameter("JwtTokenExpire", secret: true);
var jwtRefreshExpire = builder.AddParameter("JwtRefreshExpire", secret: true);

var postgresUser = builder.AddParameter("PostgresUsername", secret: true);
var postgresPassword = builder.AddParameter("PostgresPassword", secret: true);

#endregion

#region Databases

var sql = builder.AddIdentityDatabase(password: sqlPassword);

var postgres = builder.AddCatalogDatabase(username: postgresUser, password: postgresPassword);

#endregion

#region Services

builder.AddIdentityService(sql, jwtKey, jwtIssuer, jwtAudience, jwtTokenExpire, jwtRefreshExpire);

builder.AddCatalogService(postgres, jwtKey, jwtIssuer, jwtAudience);

builder.AddOrderService(jwtKey, jwtIssuer, jwtAudience);

builder.AddProductService(jwtKey, jwtIssuer, jwtAudience);

#endregion

builder.Build().Run();