using Asp.Versioning.ApiExplorer;

namespace Europhonium.WebApi.OpenApi;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
public static class Configuration
{
    /// <summary>
    ///     Adds the Swagger UI, with a separate document for each API version.
    /// </summary>
    /// <param name="app">The web application.</param>
    public static void UseVersionedSwaggerUI(this WebApplication app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/public-v1/swagger.json", "Public V1");

            // foreach (ApiVersionDescription description in app.DescribeApiVersions())
            // {
            //     var url = $"/swagger/{description.GroupName}/swagger.json";
            //     var name = description.GroupName.ToUpperInvariant();
            //
            //     options.SwaggerEndpoint(url, name);
            // }
        });
    }
}
