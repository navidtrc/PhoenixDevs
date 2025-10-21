using Microsoft.Extensions.Logging;
using Phoenix.Domain.Aggregates.User.Events;

namespace Phoenix.Application.User.EventHandlers;

public class UserPlanUpdatedEventHandler(ILogger<UserPlanUpdatedEventHandler> logger)
    : INotificationHandler<UserPlanUpdatedEvent>
{
    public Task Handle(UserPlanUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("User:{userId} plan updated", notification.User.Id);
        return Task.CompletedTask;
    }
}