var builder = DistributedApplication.CreateBuilder(args);

var sql_password = builder.AddParameter("SQLPassword", secret: true);

var sql = builder
    .AddSqlServer("sql", password: sql_password)
    .WithDataVolume()
    .AddDatabase("sqlconnection", "onlineshop_identity");

builder.AddProject<Projects.Identity_API>("identity-api")
    .WithReference(sql);

builder.AddProject<Projects.Catalog_API>("catalog-api");

builder.AddProject<Projects.Order_API>("order-api");

builder.AddProject<Projects.Product_API>("product-api");

builder.Build().Run();
