using ErrorOr;
using Europhonium.Shared.Domain.Abstractions;
using Europhonium.Shared.Domain.Broadcasts;
using Europhonium.Shared.Domain.Contests;

namespace Europhonium.Shared.Domain.Countries;

/// <summary>
///     Represents a country aggregate.
/// </summary>
public sealed class Country : AggregateRoot<CountryId>
{
    private readonly List<BroadcastId> _competingBroadcastIds = [];
    private readonly List<ContestId> _participatingContestIds = [];
    private readonly List<BroadcastId> _votingBroadcastIds = [];

    private Country()
    {
    }

    private Country(CountryId id, CountryCode countryCode, string name) : base(id)
    {
        CountryCode = countryCode;
        Name = name;
    }

    /// <summary>
    ///     Gets this country's ISO 3166-1 alpha-2 country code.
    /// </summary>
    public CountryCode CountryCode { get; private init; } = default!;

    /// <summary>
    ///     Gets this country's name.
    /// </summary>
    public string Name { get; private init; } = default!;

    /// <summary>
    ///     Gets an ordered, immutable collection containing the IDs of all the contests in which this country is a
    ///     participating country.
    /// </summary>
    /// <remarks>This method creates and returns a copy of the instance's internal collection.</remarks>
    public IReadOnlyCollection<ContestId> ParticipatingContestIds =>
        _participatingContestIds.OrderBy(id => id).ToArray().AsReadOnly();

    /// <summary>
    ///     Gets an ordered, immutable collection containing the IDs of all the broadcasts in which this country is a competing
    ///     country.
    /// </summary>
    /// <remarks>This method creates and returns a copy of the instance's internal collection.</remarks>
    public IReadOnlyCollection<BroadcastId> CompetingBroadcastIds =>
        _competingBroadcastIds.OrderBy(id => id).ToArray().AsReadOnly();

    /// <summary>
    ///     Gets an ordered, immutable collection containing the IDs of all the broadcasts in which this country is a voting
    ///     country.
    /// </summary>
    /// <remarks>This method creates and returns a copy of the instance's internal collection.</remarks>
    public IReadOnlyCollection<BroadcastId> VotingBroadcastIds => _votingBroadcastIds.OrderBy(id => id).ToArray().AsReadOnly();

    /// <summary>
    ///     Adds the specified <see cref="ContestId" /> value to this instance's <see cref="ParticipatingContestIds" />
    ///     collection if it is not already present.
    /// </summary>
    /// <remarks>
    ///     Invoking this method means that there exists a contest with an ID equal to the <paramref name="contestId" />
    ///     parameter in which this country is a participating country.
    /// </remarks>
    /// <param name="contestId">The ID to be added.</param>
    /// <exception cref="ArgumentNullException"><paramref name="contestId" /> is <see langword="null" />.</exception>
    public void AddContestId(ContestId contestId)
    {
        ArgumentNullException.ThrowIfNull(contestId);

        if (!_participatingContestIds.Contains(contestId))
        {
            _participatingContestIds.Add(contestId);
        }
    }

    /// <summary>
    ///     Adds the specified <see cref="BroadcastId" /> value to this instance's <see cref="CompetingBroadcastIds" /> and
    ///     <see cref="VotingBroadcastIds" /> collections if it is not already present in each.
    /// </summary>
    /// <remarks>
    ///     Invoking this method means that there exists a broadcast with an ID equal to the
    ///     <paramref name="broadcastId" /> parameter in which this country is a competing country and a voting country.
    /// </remarks>
    /// <param name="broadcastId">The ID to be added.</param>
    /// <exception cref="ArgumentNullException"><paramref name="broadcastId" /> is <see langword="null" />.</exception>
    public void AddBroadcastId(BroadcastId broadcastId)
    {
        ArgumentNullException.ThrowIfNull(broadcastId);

        if (!_competingBroadcastIds.Contains(broadcastId))
        {
            _competingBroadcastIds.Add(broadcastId);
        }

        if (!_votingBroadcastIds.Contains(broadcastId))
        {
            _votingBroadcastIds.Add(broadcastId);
        }
    }

    /// <summary>
    ///     Adds the specified <see cref="BroadcastId" /> value to this instance's <see cref="VotingBroadcastIds" />
    ///     collections if it is not already present.
    /// </summary>
    /// <remarks>
    ///     Invoking this method means that there exists a broadcast with an ID equal to the
    ///     <paramref name="broadcastId" /> parameter in which this country is a voting country but not a competing country.
    /// </remarks>
    /// <param name="broadcastId">The ID to be added.</param>
    /// <exception cref="ArgumentNullException"><paramref name="broadcastId" /> is <see langword="null" />.</exception>
    public void AddBroadcastIdAsVotingOnly(BroadcastId broadcastId)
    {
        ArgumentNullException.ThrowIfNull(broadcastId);

        if (!_competingBroadcastIds.Contains(broadcastId))
        {
            _competingBroadcastIds.Add(broadcastId);
        }

        if (!_votingBroadcastIds.Contains(broadcastId))
        {
            _votingBroadcastIds.Add(broadcastId);
        }
    }

    /// <summary>
    ///     Removes the specified <see cref="ContestId" /> value from this instance's <see cref="ParticipatingContestIds" />
    ///     collection if it is present.
    /// </summary>
    /// <remarks>
    ///     Invoking this method means that there no longer exists a contest with an ID equal to the
    ///     <paramref name="contestId" /> parameter in which this country is a participating country.
    /// </remarks>
    /// <param name="contestId">The ID to be removed.</param>
    /// <exception cref="ArgumentNullException"><paramref name="contestId" /> is <see langword="null" />.</exception>
    public void RemoveContestId(ContestId contestId)
    {
        ArgumentNullException.ThrowIfNull(contestId);

        _participatingContestIds.Remove(contestId);
    }

    /// <summary>
    ///     Removes the specified <see cref="BroadcastId" /> value from this instance's <see cref="CompetingBroadcastIds" />
    ///     and <see cref="VotingBroadcastIds" /> collections if it is present in either.
    /// </summary>
    /// <remarks>
    ///     Invoking this method means that there no longer exists a broadcast with an ID equal to the
    ///     <paramref name="broadcastId" /> parameter in which this country is a competing country and a voting country.
    /// </remarks>
    /// <param name="broadcastId">The ID to be removed.</param>
    /// <exception cref="ArgumentNullException"><paramref name="broadcastId" /> is <see langword="null" />.</exception>
    public void RemoveBroadcastId(BroadcastId broadcastId)
    {
        ArgumentNullException.ThrowIfNull(broadcastId);

        _competingBroadcastIds.Remove(broadcastId);
        _votingBroadcastIds.Remove(broadcastId);
    }

    /// <summary>
    ///     Starts the process of creating a new <see cref="Country" /> instance using the fluent builder API.
    /// </summary>
    /// <returns>An <see cref="ICountryBuilder" /> instance.</returns>
    public static ICountryBuilder Create() => new Builder();

    private sealed class Builder : ICountryBuilder, ICountryBuilder.INameSetter, ICountryBuilder.ITerminal
    {
        private string? _countryCode;
        private string? _name;

        /// <inheritdoc />
        public ICountryBuilder.INameSetter WithCountryCode(string countryCode)
        {
            _countryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));

            return this;
        }

        /// <inheritdoc />
        public ICountryBuilder.ITerminal AndName(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));

            return this;
        }

        /// <inheritdoc />
        public ErrorOr<Country> Build()
        {
            return CountryCode.FromValue(_countryCode!)
                .Then(countryCode => (Id: CountryId.Unique(), CountryCode: countryCode, Name: _name!))
                .Then(tuple => new Country(tuple.Id, tuple.CountryCode, tuple.Name));
        }
    }
}
