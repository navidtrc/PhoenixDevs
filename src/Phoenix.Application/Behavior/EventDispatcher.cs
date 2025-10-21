using Framework.Core.Domain.Abstractions;
using MediatR;

namespace Phoenix.Application.Behavior;

public class EventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    public async Task Dispatch(IDomainEvent domainEvent)
    {
        await mediator.Publish(domainEvent);
    }
}
