using ErrorOr;

namespace Europhonium.Shared.Domain.Countries;

/// <summary>
///     Fluent builder for the <see cref="Country" /> aggregate root entity type.
/// </summary>
public interface ICountryBuilder
{
    /// <summary>
    ///     Sets the <see cref="Country.CountryCode" /> property value of the instance.
    /// </summary>
    /// <param name="countryCode">A string of 2 upper-case ASCII letters. The country's ISO 3166-1 alpha-2 country code.</param>
    /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="countryCode" /> is <see langword="null" />.</exception>
    public INameSetter WithCountryCode(string countryCode);

    /// <summary>
    ///     Fluent builder for the <see cref="Country" /> aggregate root entity type.
    /// </summary>
    public interface INameSetter
    {
        /// <summary>
        ///     Sets the <see cref="Country.Name" /> property value of the instance.
        /// </summary>
        /// <param name="name">The country's name.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name" /> is <see langword="null" />.</exception>
        public ITerminal AndName(string name);
    }

    /// <summary>
    ///     Fluent builder for the <see cref="Country" /> aggregate root entity type.
    /// </summary>
    public interface ITerminal
    {
        /// <summary>
        ///     Creates and returns a valid <see cref="Country" /> instance from the previous fluent builder method invocations.
        /// </summary>
        /// <remarks>
        ///     The newly instantiated <see cref="Country" /> instance has empty
        ///     <see cref="Country.ParticipatingContestIds" />, <see cref="Country.CompetingBroadcastIds" /> and
        ///     <see cref="Country.VotingBroadcastIds" /> collections.
        /// </remarks>
        /// <returns>
        ///     One of:
        ///     <list type="bullet">
        ///         <item>A new <see cref="Country" /> instance, <i>or</i></item>
        ///         <item>An <see cref="Error" /> instance reporting a validation failure.</item>
        ///     </list>
        /// </returns>
        public ErrorOr<Country> Build();
    }
}
