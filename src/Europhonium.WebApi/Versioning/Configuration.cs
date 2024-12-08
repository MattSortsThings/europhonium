using Asp.Versioning;
using Asp.Versioning.Builder;

namespace Europhonium.WebApi.Versioning;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
internal static class Configuration
{
    /// <summary>
    ///     Creates the API version sets for the web application.
    /// </summary>
    /// <param name="builder">The root-level endpoint route builder for the web application.</param>
    /// <returns>A new <see cref="ApiVersionSet" /> instance configured on the <paramref name="builder" /> parameter.</returns>
    internal static ApiVersionSet CreateVersionSets(this IEndpointRouteBuilder builder) =>
        builder.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1, 0))
            .Build();
}
