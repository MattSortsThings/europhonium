using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Endpoints.Shared.ErrorHandling;

internal static class ErrorExtensions
{
    /// <summary>
    ///     Maps the <see cref="Error" /> instance to a <see cref="ProblemDetails" /> object and returns a
    ///     <see cref="ProblemHttpResult" /> containing the latter.
    /// </summary>
    /// <param name="error">The error to be mapped.</param>
    /// <returns>A new <see cref="ProblemHttpResult" /> instance.</returns>
    internal static ProblemHttpResult ToProblemHttpResult(this Error error) => TypedResults.Problem(error.Description,
        statusCode: error.Type.ToStatusCode(),
        title: error.Code,
        extensions: error.Metadata.ToExtensions());

    private static int ToStatusCode(this ErrorType errorType) => errorType switch
    {
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
        _ => StatusCodes.Status500InternalServerError
    };

    private static Dictionary<string, object?> ToExtensions(this IDictionary<string, object>? errorMetadata)
    {
        if (errorMetadata is null)
        {
            return [];
        }

        Dictionary<string, object?> extensions = new(errorMetadata.Count);
        foreach (var (key, value) in errorMetadata)
        {
            extensions.Add(key, value);
        }

        return extensions;
    }
}
