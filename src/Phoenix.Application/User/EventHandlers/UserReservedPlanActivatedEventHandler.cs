using Microsoft.Extensions.Logging;
using Phoenix.Domain.Aggregates.User.Events;

namespace Phoenix.Application.User.EventHandlers;

public class UserReservedPlanActivatedEventHandler(ILogger<UserReservedPlanActivatedEventHandler> logger)
    : INotificationHandler<UserReservedPlanActivatedEvent>
{
    public Task Handle(UserReservedPlanActivatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("User:{userId} reserved plan activated", notification.User.Id);
        return Task.CompletedTask;
    }
}