using Europhonium.Web;

WebApplication.CreateBuilder(args)
    .AddServices()
    .Build()
    .ConfigureRequestPipeline()
    .Run();
