using Europhonium.WebApi.Security;
using Microsoft.OpenApi.Models;

namespace Europhonium.WebApi.OpenApi;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Registers the OpenAPI Swagger document generation services and settings for the web application.
    /// </summary>
    /// <param name="services">Contains service descriptors for the web application.</param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, so that method invocations can be chained.</returns>
    public static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        services.ConfigureOptions<SwaggerGenOptionsConfiguration>()
            .AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(SwaggerFunctions.SchemaIdGenerator);

                options.AddSecurityDefinition(ApiKeySecurityConstants.DefinitionName, new OpenApiSecurityScheme
                {
                    Description = "API key security.",
                    Type = SecuritySchemeType.ApiKey,
                    Name = ApiKeySecurityConstants.HttpRequestHeaderName,
                    In = ParameterLocation.Header,
                    Scheme = ApiKeySecurityConstants.SchemeName
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = ApiKeySecurityConstants.DefinitionName
                            },
                            In = ParameterLocation.Header
                        },
                        []
                    }
                });
            });

        return services;
    }
}
