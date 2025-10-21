using Microsoft.Extensions.Logging;
using Phoenix.Domain.Aggregates.User.Events;

namespace Phoenix.Application.User.EventHandlers;

public class UserUpdatedEventHandler(ILogger<UserUpdatedEventHandler> logger)
    : INotificationHandler<UserUpdatedEvent>
{
    public Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("User:{userId} created", notification.User.Id);
        return Task.CompletedTask;
    }
}