using ErrorOr;
using Europhonium.Endpoints.Shared.ErrorHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Europhonium.Endpoints.Shared.Endpoints;

internal abstract class RailwayEndpoint<TRequest,
    TAppRequest,
    TAppResult,
    TResult> : Endpoint<TRequest, Results<TResult, ProblemHttpResult>>
    where TRequest : notnull
    where TResult : IResult
    where TAppRequest : IRequest<ErrorOr<TAppResult>>
{
    private readonly ISender _sender;

    protected RailwayEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override async Task<Results<TResult, ProblemHttpResult>> ExecuteAsync(TRequest request, CancellationToken ct)
    {
        ErrorOr<TAppResult> result = await _sender.Send(MapToAppRequest(request), ct);

        return result.IsError ? result.FirstError.ToProblemHttpResult() : MapToResult(result.Value);
    }

    private protected abstract TAppRequest MapToAppRequest(TRequest request);

    private protected abstract TResult MapToResult(TAppResult appResult);
}
