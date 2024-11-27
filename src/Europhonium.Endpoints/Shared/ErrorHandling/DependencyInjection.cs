using Microsoft.Extensions.DependencyInjection;

namespace Europhonium.Endpoints.Shared.ErrorHandling;

internal static class DependencyInjection
{
    /// <summary>
    ///     RegisterS the error handling services for the endpoints.
    /// </summary>
    /// <param name="services">Contains service descriptors for the web application.</param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, so that method invocations can be chained.</returns>
    internal static IServiceCollection AddErrorHandlingServices(this IServiceCollection services)
    {
        services.AddProblemDetails(options => options.CustomizeProblemDetails = context =>
        {
            context.ProblemDetails.Extensions.Add("traceId",
                context.HttpContext.TraceIdentifier);

            context.ProblemDetails.Extensions.Add("instance",
                $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}");
        }).AddExceptionHandler<ExceptionToProblemDetailsConverter>();

        return services;
    }
}
