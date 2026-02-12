using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<OnlineShop_Identity>("Identity");

builder.Build().Run();
