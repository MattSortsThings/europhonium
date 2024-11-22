using FastEndpoints;
using Microsoft.AspNetCore.Builder;

namespace Europhonium.Endpoints;

public static class Configuration
{
    /// <summary>
    ///     Configures all the endpoints for the web application's request pipeline.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <returns>The same <see cref="WebApplication" /> instance, so that method invocations can be chained.</returns>
    public static WebApplication UseEndpoints(this WebApplication app)
    {
        app.UseFastEndpoints();

        return app;
    }
}
