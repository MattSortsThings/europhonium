using Microsoft.AspNetCore.Diagnostics;

namespace Europhonium.WebApi.ErrorHandling;

/// <summary>
///     Creates a problem details HTTP response from an uncaught exception.
/// </summary>
/// <remarks>
///     This class is adapted from
///     <a href="https://timdeschryver.dev/blog/translating-exceptions-into-problem-details-responses">code</a> by Tim
///     Deschruyver.
/// </remarks>
internal sealed class ExceptionToProblemDetailsHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    /// <inheritdoc />
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Title = "InternalServerError",
                Status = StatusCodes.Status500InternalServerError,
                Detail = $"{exception.GetType().Name} was thrown while attempting to handle the request."
            }
        });
    }
}
