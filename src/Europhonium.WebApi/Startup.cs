using Europhonium.Modules.Admin;
using Europhonium.Modules.Public;
using Europhonium.Shared.Infrastructure;

namespace Europhonium.WebApi;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
internal static class Startup
{
    /// <summary>
    ///     Registers all the services for the web application.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    /// <returns>The same <see cref="WebApplicationBuilder" /> instance, so that method invocations can be chained.</returns>
    internal static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(configuration =>
                configuration.AddAdminModuleMediatRServices()
                    .AddPublicModuleMediatRServices())
            .AddSharedInfrastructureServices();

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

        app.UseEndpoints();

        return app;
    }

    private static void UseEndpoints(this WebApplication app)
    {
        app.MapGroup("api").MapAdminEndpoints().MapPublicEndpoints();
    }
}
