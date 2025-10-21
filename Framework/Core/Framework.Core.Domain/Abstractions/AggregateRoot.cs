using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Core.Domain.Abstractions;

public abstract class AggregateRoot<TId> : BaseEntity<TId>, IAggregateRoot where TId : struct,
    IComparable,
    IComparable<TId>,
    IEquatable<TId>,
    IFormattable
{
    private readonly List<IDomainEvent> _events;
    protected AggregateRoot() => _events = new();

    private List<IDomainEvent> _domainEvents;
    [NotMapped]
    public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents = _domainEvents ?? new List<IDomainEvent>();
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents?.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}

public abstract class AggregateRoot : AggregateRoot<Ulid>
{
    protected AggregateRoot()
    {
        Id = Ulid.NewUlid();
    }
}
