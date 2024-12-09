using Europhonium.Shared.Domain.Abstractions;

namespace Europhonium.Shared.Domain.Countries;

/// <summary>
///     Identifier for a country entity.
/// </summary>
public sealed class CountryId : ValueObject, IComparable<CountryId>
{
    private CountryId(Guid value)
    {
        Value = value;
    }

    /// <summary>
    ///     Gets the underlying unique identifier value of this instance.
    /// </summary>
    public Guid Value { get; }

    /// <inheritdoc />
    /// <remarks>Two <see cref="CountryId" /> instances are compared by their <see cref="Value" /> properties.</remarks>
    public int CompareTo(CountryId? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        return other is null ? 1 : Value.CompareTo(other.Value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    /// <summary>
    ///     Creates and returns a new <see cref="CountryId" /> instance with a newly-generated unique identifier
    ///     <see cref="Value" />.
    /// </summary>
    /// <returns>A new <see cref="CountryId" /> instance.</returns>
    public static CountryId Unique() => new(Guid.NewGuid());

    /// <summary>
    ///     Creates and returns a new <see cref="CountryId" /> instance with the specified <see cref="Value" />.
    /// </summary>
    /// <param name="value">The underlying unique identifier value of the instance.</param>
    /// <returns>A new <see cref="CountryId" /> instance.</returns>
    public static CountryId FromValue(Guid value) => new(value);
}
