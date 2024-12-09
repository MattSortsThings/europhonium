namespace Europhonium.Modules.Admin.Countries;

public sealed record CountryResource(
    Guid Id,
    string CountryCode,
    string CountryName,
    Guid[] ParticipatingContestIds,
    Guid[] CompetingBroadcastIds,
    Guid[] VotingBroadcastIds);
