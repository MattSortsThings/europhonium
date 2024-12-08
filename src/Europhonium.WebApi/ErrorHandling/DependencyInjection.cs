namespace Europhonium.WebApi.ErrorHandling;

/// <summary>
///     Extension methods to be invoked at application startup.
/// </summary>
internal static class DependencyInjection
{
    /// <summary>
    ///     Registers the error handling services for the web application.
    /// </summary>
    /// <param name="services">Contains service descriptors for the web application.</param>
    /// <returns>The same <see cref="IServiceCollection" /> instance, so that method invocations can be chained.</returns>
    /// <returns></returns>
    internal static IServiceCollection AddErrorHandling(this IServiceCollection services)
    {
        services.AddProblemDetails(options => options.CustomizeProblemDetails = ctx =>
        {
            ctx.ProblemDetails.Extensions.Add("traceId", ctx.HttpContext.TraceIdentifier);
            ctx.ProblemDetails.Extensions.Add("instance",
                $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
        }).AddExceptionHandler<ExceptionToProblemDetailsHandler>();

        return services;
    }
}
