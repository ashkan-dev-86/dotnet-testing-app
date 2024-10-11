var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.DotNetTestingApp_Aspire_ApiService>("apiservice");

builder.AddProject<Projects.DotNetTestingApp_Aspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
