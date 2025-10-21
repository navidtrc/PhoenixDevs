using Framework.Core.Domain.Abstractions;

namespace Phoenix.Domain.Aggregates.User.Events;

public class UserUpdatedEvent(User user) : DomainEvent
{
    public User User { get; set; } = user;
}