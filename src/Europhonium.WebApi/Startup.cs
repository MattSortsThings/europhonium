using Europhonium.Application;
using Europhonium.Endpoints;
using Europhonium.Infrastructure;
using FastEndpoints.Swagger;

namespace Europhonium.WebApi;

internal static class Startup
{
    /// <summary>
    ///     Registers all the services for the web application.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    /// <returns>The same <see cref="WebApplicationBuilder" /> instance, so that method invocations can be chained.</returns>
    internal static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices()
            .AddInfrastructureServices()
            .AddEndpointsServices(builder.Configuration);

        return builder;
    }

    /// <summary>
    ///     Configures the HTTP request pipeline for the web application.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <returns>The same <see cref="WebApplication" /> instance, so that method invocations can be chained.</returns>
    internal static WebApplication ConfigureRequestPipeline(this WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseStatusCodePages();

        app.UseExceptionHandler();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseSwaggerGen();

        app.UseEndpoints();

        return app;
    }
}
