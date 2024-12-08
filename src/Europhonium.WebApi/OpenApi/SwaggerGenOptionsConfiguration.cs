using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Europhonium.WebApi.OpenApi;

/// <summary>
///     Configures a Swagger document for every API version set.
/// </summary>
/// <remarks>
///     This class is adapted from a <a href="https://youtu.be/F9j3X6KuIpw?si=k0CG7VOyNQwMV64m">video tutorial</a> by Milan
///     Jovanovic.
/// </remarks>
/// <param name="provider">Provides API version information for the web application.</param>
internal sealed class SwaggerGenOptionsConfiguration(IApiVersionDescriptionProvider provider) :
    IConfigureNamedOptions<SwaggerGenOptions>
{
    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {
        foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
        {
            OpenApiInfo info = new()
            {
                Title = $"Europhonium API v{description.ApiVersion.MajorVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "A web API for (over)analysing the Eurovision Song Contest."
            };

            options.SwaggerDoc(description.GroupName, info);
        }
    }

    /// <inheritdoc />
    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }
}
