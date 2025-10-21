using Microsoft.Extensions.Logging;
using Phoenix.Domain.Aggregates.User.Events;

namespace Phoenix.Application.User.EventHandlers;

public class UserReservedPlanUpdatedEventHandler(ILogger<UserReservedPlanUpdatedEventHandler> logger)
    : INotificationHandler<UserReservedPlanUpdatedEvent>
{
    public Task Handle(UserReservedPlanUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("User:{userId} reserved plan updated", notification.User.Id);
        return Task.CompletedTask;
    }
}