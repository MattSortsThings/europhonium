using Microsoft.Extensions.DependencyInjection;

namespace Europhonium.Modules.Public;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Registers all the MediatR services for the Public Module.
    /// </summary>
    /// <param name="config">Contains MediatR service configuration for the web application.</param>
    /// <returns>
    ///     The same <see cref="MediatRServiceConfiguration" /> instance, so that method invocations can be chained.
    /// </returns>
    public static MediatRServiceConfiguration AddPublicModuleMediatRServices(this MediatRServiceConfiguration config)
    {
        config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

        return config;
    }
}
