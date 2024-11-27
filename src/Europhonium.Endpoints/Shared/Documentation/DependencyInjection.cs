using System.Text.Json;
using System.Text.Json.Serialization;
using Europhonium.Endpoints.Admin;
using Europhonium.Endpoints.Public;
using FastEndpoints.Swagger;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NJsonSchema.Generation.TypeMappers;
using NSwag.Generation.AspNetCore;

namespace Europhonium.Endpoints.Shared.Documentation;

internal static class DependencyInjection
{
    /// <summary>
    ///     Registers the Swagger documentation services for the web application.
    /// </summary>
    /// <param name="services">Contains service descriptors for the web application.</param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, so that method invocations can be chained.</returns>
    internal static IServiceCollection AddDocumentationServices(this IServiceCollection services)
    {
        services.SwaggerDocument(options =>
        {
            options.EndpointFilter = definition => definition.EndpointTags?.Contains(nameof(PublicEndpointGroup)) is true;

            options.DocumentSettings = settings =>
            {
                settings.DocumentName = DocumentationConstants.DocumentNames.PublicVersion1;
                settings.Title = "Europhonium Public API";
                settings.Version = "v1";
                settings.AddTypeMappers();
            };

            options.EnableJWTBearerAuth = false;

            options.ShortSchemaNames = true;

            options.RemoveEmptyRequestSchema = true;

            options.AddSerializerSettings();
        }).SwaggerDocument(options =>
        {
            options.EndpointFilter = definition => definition.EndpointTags?.Contains(nameof(AdminEndpointGroup)) is true;

            options.DocumentSettings = settings =>
            {
                settings.DocumentName = DocumentationConstants.DocumentNames.AdminVersion1;
                settings.Title = "Europhonium Admin API";
                settings.Version = "v1";
                settings.AddTypeMappers();
            };

            options.EnableJWTBearerAuth = false;

            options.ShortSchemaNames = true;

            options.RemoveEmptyRequestSchema = true;

            options.AddSerializerSettings();
        });

        return services;
    }

    private static void AddTypeMappers(this AspNetCoreOpenApiDocumentGeneratorSettings settings)
    {
        settings.SchemaSettings.TypeMappers.Add(new PrimitiveTypeMapper(
            typeof(Guid),
            schema =>
            {
                schema.Type = JsonObjectType.String;
                schema.Format = "uuid";
            }));
    }

    private static void AddSerializerSettings(this DocumentOptions options)
    {
        options.SerializerSettings = settings =>
        {
            settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            settings.Converters.Add(new JsonStringEnumConverter());
        };
    }
}
