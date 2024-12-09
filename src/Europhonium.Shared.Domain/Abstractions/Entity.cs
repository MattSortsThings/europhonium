namespace Europhonium.Shared.Domain.Abstractions;

/// <summary>
///     Generic abstract base class for a domain entity.
/// </summary>
/// <typeparam name="TId">The domain entity identifier type.</typeparam>
public abstract class Entity<TId>
    where TId : ValueObject
{
    /// <summary>
    ///     Parameterless constructor for EF Core.
    /// </summary>
    protected Entity()
    {
    }

    protected Entity(TId id)
    {
        Id = id;
    }

    /// <summary>
    ///     Gets the identifier for this instance.
    /// </summary>
    public TId Id { get; } = default!;

    /// <summary>
    ///     Returns an equality comparer that uses the two objects' <see cref="Entity{TId}.Id" /> values as the basis for
    ///     equality comparison.
    /// </summary>
    public static IEqualityComparer<Entity<TId>> IdComparer { get; } = new IdEqualityComparer();

    private sealed class IdEqualityComparer : IEqualityComparer<Entity<TId>>
    {
        public bool Equals(Entity<TId>? x, Entity<TId>? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x is null)
            {
                return false;
            }

            if (y is null)
            {
                return false;
            }

            return x.GetType() == y.GetType() && EqualityComparer<TId>.Default.Equals(x.Id, y.Id);
        }

        public int GetHashCode(Entity<TId> obj) => EqualityComparer<TId>.Default.GetHashCode(obj.Id);
    }
}
