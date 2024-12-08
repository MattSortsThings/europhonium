using Asp.Versioning;

namespace Europhonium.WebApi.Versioning;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
internal static class DependencyInjection
{
    /// <summary>
    ///     Registers the API versioning services for the web application.
    /// </summary>
    /// <param name="services">Contains service descriptors for the web application.</param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, so that method invocations can be chained.</returns>
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }
}
