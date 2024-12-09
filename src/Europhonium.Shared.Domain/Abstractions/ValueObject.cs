namespace Europhonium.Shared.Domain.Abstractions;

/// <summary>
///     Abstract base class for a domain value object.
/// </summary>
/// <remarks>
///     <para>Two value objects are equal if their atomic values are equal.</para>
///     <para>
///         This class is adapted from a
///         <a href="https://www.milanjovanovic.tech/blog/value-objects-in-dotnet-ddd-fundamentals">blog post</a> by Milan
///         Jovanovic.
///     </para>
/// </remarks>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <inheritdoc />
    /// <remarks>Two value objects are equal if their atomic values are equal.</remarks>
    public virtual bool Equals(ValueObject? other) => other is not null && AtomicValuesAreEqual(other);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is ValueObject valueObject && AtomicValuesAreEqual(valueObject);

    /// <summary>
    ///     Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() =>
        GetAtomicValues().Aggregate(
            default(int),
            (hashcode, value) =>
                HashCode.Combine(hashcode, value.GetHashCode()));

    /// <summary>
    ///     Generates a fixed sequence of immutable atomic values for equality comparison.
    /// </summary>
    /// <remarks>
    ///     Implement this method in a concrete <see cref="ValueObject" /> derivative to set its equality comparison rules.
    /// </remarks>
    /// <returns>A fixed sequence of immutable atomic values.</returns>
    protected abstract IEnumerable<object> GetAtomicValues();

    private bool AtomicValuesAreEqual(ValueObject valueObject) =>
        GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());

    public static bool operator ==(ValueObject? a, ValueObject? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(ValueObject? a, ValueObject? b) =>
        !(a == b);
}
