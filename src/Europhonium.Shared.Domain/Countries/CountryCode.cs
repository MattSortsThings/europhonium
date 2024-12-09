using System.Text.RegularExpressions;
using ErrorOr;
using Europhonium.Shared.Domain.Abstractions;
using Europhonium.Shared.Domain.DomainErrors;

namespace Europhonium.Shared.Domain.Countries;

/// <summary>
///     A country's ISO 3166-1 alpha-2 country code.
/// </summary>
public sealed partial class CountryCode : ValueObject, IComparable<CountryCode>
{
    private CountryCode(string value)
    {
        Value = value;
    }

    /// <summary>
    ///     Gets the underlying string value of this instance.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    /// <remarks>
    ///     Two <see cref="CountryCode" /> instances are compared using their respective <see cref="Value" /> property values.
    /// </remarks>
    public int CompareTo(CountryCode? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        return other is null ? 1 : string.Compare(Value, other.Value, StringComparison.OrdinalIgnoreCase);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    /// <summary>
    ///     Creates and returns a new <see cref="CountryCode" /> instance with the specified <see cref="Value" />, which is
    ///     guaranteed to represent a valid ISO 3166-1 alpha-2 country code.
    /// </summary>
    /// <param name="value">A string of 2 upper-case letters. The underlying string value of the instance.</param>
    /// <returns>
    ///     One of:
    ///     <list type="bullet">
    ///         <item>a new <see cref="CountryCode" /> instance, <i>or</i></item>
    ///         <item>an <see cref="Error" /> instance reporting a validation failure.</item>
    ///     </list>
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="value" /> is <see langword="null" />.</exception>
    public static ErrorOr<CountryCode> FromValue(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return CountryCodeRegex().IsMatch(value) ? new CountryCode(value) : Errors.Countries.InvalidCountryCode(value);
    }

    [GeneratedRegex("^[A-Z]{2}$", RegexOptions.Compiled)]
    private static partial Regex CountryCodeRegex();
}
