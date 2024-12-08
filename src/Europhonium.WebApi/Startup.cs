using Europhonium.Modules.Admin;
using Europhonium.Modules.Public;
using Europhonium.Shared.Infrastructure;
using Europhonium.WebApi.ErrorHandling;
using Europhonium.WebApi.OpenApi;
using Europhonium.WebApi.Security;
using Europhonium.WebApi.Versioning;

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
            .AddSharedInfrastructureServices()
            .AddErrorHandling()
            .AddEndpointsApiExplorer()
            .AddVersioning()
            .AddApiKeySecurity(builder.Configuration)
            .AddOpenApi();

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

        app.UseVersionedSwaggerUI();

        app.UseVersionedEndpoints();

        return app;
    }

    private static void UseVersionedEndpoints(this WebApplication app)
    {
        RouteGroupBuilder versionedEndpointsGroup =
            app.MapGroup("api/v{apiVersion:apiVersion}")
                .WithApiVersionSet(app.CreateVersionSets());

        versionedEndpointsGroup.MapPublicEndpoints().MapAdminEndpoints();
    }
}
