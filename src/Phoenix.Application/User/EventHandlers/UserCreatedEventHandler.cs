using Microsoft.Extensions.Logging;
using Phoenix.Domain.Aggregates.User.Events;

namespace Phoenix.Application.User.EventHandlers;

public class UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
    : INotificationHandler<UserCreatedEvent>
{
    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("User:{userId} created", notification.User.Id);
        return Task.CompletedTask;
    }
}