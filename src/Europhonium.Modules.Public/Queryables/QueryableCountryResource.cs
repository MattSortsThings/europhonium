namespace Europhonium.Modules.Public.Queryables;

public sealed record QueryableCountryResource
{
    public required string CountryCode { get; init; }

    public required string Name { get; init; }

    public required int ParticipatingContests { get; init; }

    public required int CompetingBroadcasts { get; init; }

    public required int VotingBroadcasts { get; init; }
}
