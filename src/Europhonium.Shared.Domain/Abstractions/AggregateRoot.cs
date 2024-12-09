namespace Europhonium.Shared.Domain.Abstractions;

/// <summary>
///     Generic abstract base class for a domain aggregate root entity.
/// </summary>
/// <typeparam name="TId">The domain aggregate root entity identifier type.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : ValueObject
{
    /// <summary>
    ///     Parameterless constructor for EF Core.
    /// </summary>
    protected AggregateRoot()
    {
    }

    protected AggregateRoot(TId id) : base(id)
    {
    }
}
