using Europhonium.Shared.Domain.Abstractions;

namespace Europhonium.Shared.Domain.Broadcasts;

/// <summary>
///     Identifier for a broadcast entity.
/// </summary>
public sealed class BroadcastId : ValueObject, IComparable<BroadcastId>
{
    private BroadcastId(Guid value)
    {
        Value = value;
    }

    /// <summary>
    ///     Gets the underlying unique identifier value of this instance.
    /// </summary>
    public Guid Value { get; }

    /// <inheritdoc />
    /// <remarks>Two <see cref="BroadcastId" /> instances are compared by their <see cref="Value" /> properties.</remarks>
    public int CompareTo(BroadcastId? other)
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
    ///     Creates and returns a new <see cref="BroadcastId" /> instance with a newly-generated unique identifier
    ///     <see cref="Value" />.
    /// </summary>
    /// <returns>A new <see cref="BroadcastId" /> instance.</returns>
    public static BroadcastId Unique() => new(Guid.NewGuid());

    /// <summary>
    ///     Creates and returns a new <see cref="BroadcastId" /> instance with the specified <see cref="Value" />.
    /// </summary>
    /// <param name="value">The underlying unique identifier value of the instance.</param>
    /// <returns>A new <see cref="BroadcastId" /> instance.</returns>
    public static BroadcastId FromValue(Guid value) => new(value);
}
