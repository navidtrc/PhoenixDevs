using MediatR;
using System.Threading.Tasks;

namespace Framework.Core.Domain.Abstractions;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
public abstract class DomainEvent : IDomainEvent, INotification
{
    public DateTime OccurredOn => DateTime.Now;
}
public interface IDomainEventDispatcher
{
    Task Dispatch(IDomainEvent domainEvent);
}