using Europhonium.Shared.Domain.Countries;

namespace Europhonium.Modules.Admin.Countries;

internal static class ResourceMapping
{
    internal static CountryResource ToCountryResource(this Country country)
    {
        return new CountryResource(country.Id.Value,
            country.CountryCode.Value,
            country.Name,
            country.ParticipatingContestIds.Select(id => id.Value).ToArray(),
            country.CompetingBroadcastIds.Select(id => id.Value).ToArray(),
            country.VotingBroadcastIds.Select(id => id.Value).ToArray());
    }
}
