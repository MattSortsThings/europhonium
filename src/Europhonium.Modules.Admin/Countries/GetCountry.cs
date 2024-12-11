using ErrorOr;
using Europhonium.Shared.Domain.Countries;
using Europhonium.Shared.Domain.DomainErrors;
using Europhonium.Shared.Infrastructure.DataAccess.EFCore;
using Europhonium.Shared.Infrastructure.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Europhonium.Modules.Admin.Countries;

public static class GetCountry
{
    public static async Task<Results<Ok<Response>, ProblemHttpResult>> ExecuteAsync([FromRoute] Guid countryId,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(countryId.ToQuery(), cancellationToken);

        return result.IsError ? result.FirstError.ToProblemHttpResult() : result.Value.ToOkResult();
    }

    public sealed record Response(CountryResource Country);

    private sealed record Query(CountryId CountryId) : IRequest<ErrorOr<CountryResource>>;

    private sealed record Handler(WebAppDbContext dbContext) : IRequestHandler<Query, ErrorOr<CountryResource>>
    {
        public async Task<ErrorOr<CountryResource>> Handle(Query query, CancellationToken cancellationToken)
        {
            Country? match = await dbContext.Countries
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(country => country.Id.Equals(query.CountryId), cancellationToken);

            return match is not null ? match.ToCountryResource() : Errors.Countries.CountryNotFound(query.CountryId);
        }
    }

    private static Query ToQuery(this Guid countryId) => new Query(countryId.ToCountryId());

    private static Ok<Response> ToOkResult(this CountryResource country) => TypedResults.Ok(new Response(country));
}
