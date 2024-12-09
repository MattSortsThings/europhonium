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

public static class CreateCountry
{
    public static async Task<Results<Ok<Response>, ProblemHttpResult>> ExecuteAsync(
        [FromBody] Request request,
        ISender sender,
        CancellationToken cancellationToken = default)
    {
        ErrorOr<CountryResource> result = await sender.Send(request.ToCommand(), cancellationToken);

        return result.IsError ? result.FirstError.ToProblemHttpResult() : result.Value.ToOkResult();
    }

    private static Command ToCommand(this Request request) => new(request.CountryCode, request.Name);

    private static Ok<Response> ToOkResult(this CountryResource country) => TypedResults.Ok(new Response(country));

    public sealed record Request
    {
        public string CountryCode { get; init; } = string.Empty;

        public string Name { get; init; } = string.Empty;
    }

    public sealed record Response(CountryResource Country);

    private sealed record Command(string CountryCode, string Name) : IRequest<ErrorOr<CountryResource>>;

    private sealed class Handler(WebAppDbContext dbContext) : IRequestHandler<Command, ErrorOr<CountryResource>>
    {
        public async Task<ErrorOr<CountryResource>> Handle(Command command, CancellationToken cancellationToken) =>
            await Country.Create()
                .WithCountryCode(command.CountryCode)
                .AndName(command.Name)
                .Build()
                .FailIf(CountryCodeConflict, Errors.Countries.CountryCodeConflict(command.CountryCode))
                .ThenDo(AddCountryToDatabase)
                .ThenDoAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
                .Then(country => country.ToCountryResource());

        private bool CountryCodeConflict(Country country) =>
            dbContext.Countries.AsNoTracking().Any(c => c.CountryCode.Equals(country.CountryCode));

        private void AddCountryToDatabase(Country country) =>
            dbContext.Countries.Add(country);
    }
}
