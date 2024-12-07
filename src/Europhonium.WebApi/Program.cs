using Europhonium.WebApi;

WebApplication.CreateBuilder(args)
    .AddServices()
    .Build()
    .ConfigureRequestPipeline()
    .Run();
