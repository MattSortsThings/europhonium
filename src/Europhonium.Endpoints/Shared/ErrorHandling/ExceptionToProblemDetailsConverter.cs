using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Europhonium.Endpoints.Shared.ErrorHandling;

/// <summary>
///     Creates a problem details HTTP response from an uncaught exception.
/// </summary>
/// <remarks>
///     This class is adapted from
///     <a href="https://timdeschryver.dev/blog/translating-exceptions-into-problem-details-responses">code</a> by Tim
///     Deschruyver.
/// </remarks>
internal sealed class ExceptionToProblemDetailsConverter(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var exceptionTypeName = exception.GetType().Name;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Title = "InternalServerError",
                Status = StatusCodes.Status500InternalServerError,
                Detail = $"{exceptionTypeName} was thrown while handing the request."
            }
        });
    }
}
