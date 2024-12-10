using ErrorOr;
using Europhonium.Shared.Infrastructure.DataAccess.EFCore;
using Europhonium.Shared.Infrastructure.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Europhonium.Modules.Public.Queryables;

public static class GetQueryableCountries
{
    public static async Task<Results<Ok<Response>, ProblemHttpResult>> ExecuteAsync(ISender sender,
        CancellationToken cancellationToken = default)
    {
        ErrorOr<QueryableCountryResource[]> result = await sender.Send(new Query(), cancellationToken);

        return result.IsError ? result.FirstError.ToProblemHttpResult() : result.Value.ToOkResult();
    }

    private static Ok<Response> ToOkResult(this QueryableCountryResource[] queryableCountries) =>
        TypedResults.Ok(new Response(queryableCountries));

    public sealed record Response(QueryableCountryResource[] QueryableCountries);

    internal sealed record Query : IRequest<ErrorOr<QueryableCountryResource[]>>;

    internal sealed class Handler(WebAppDbContext dbContext) : IRequestHandler<Query, ErrorOr<QueryableCountryResource[]>>
    {
        public async Task<ErrorOr<QueryableCountryResource[]>> Handle(Query query, CancellationToken cancellationToken)
        {
            return await dbContext.Countries.AsNoTracking()
                .AsSplitQuery()
                .OrderBy(country => country.CountryCode)
                .Select(country => new QueryableCountryResource
                {
                    CountryCode = country.CountryCode.Value,
                    Name = country.Name,
                    ParticipatingContests = country.ParticipatingContestIds.Count,
                    CompetingBroadcasts = country.CompetingBroadcastIds.Count,
                    VotingBroadcasts = country.VotingBroadcastIds.Count
                })
                .ToArrayAsync(cancellationToken);
        }
    }
}
