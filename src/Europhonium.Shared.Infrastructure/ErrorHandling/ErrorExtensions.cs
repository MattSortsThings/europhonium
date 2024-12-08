using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Europhonium.Shared.Infrastructure.ErrorHandling;

/// <summary>
///     Contains an extension method on the <see cref="Error" /> type.
/// </summary>
public static class ErrorExtensions
{
    /// <summary>
    ///     Maps the <see cref="Error" /> instance to a <see cref="ProblemDetails" /> object and returns a
    ///     <see cref="ProblemHttpResult" /> containing the latter.
    /// </summary>
    /// <param name="error">The error to be mapped.</param>
    /// <returns>A new <see cref="ProblemHttpResult" /> instance.</returns>
    public static ProblemHttpResult ToProblemHttpResult(this Error error) => TypedResults.Problem(error.Description,
        statusCode: error.Type.ToStatusCode(), title: error.Code,
        extensions: error.Metadata.ToExtensions());

    private static int ToStatusCode(this ErrorType errorType) => errorType switch
    {
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
        _ => StatusCodes.Status500InternalServerError
    };

    private static Dictionary<string, object?> ToExtensions(this Dictionary<string, object>? errorMetadata)
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
