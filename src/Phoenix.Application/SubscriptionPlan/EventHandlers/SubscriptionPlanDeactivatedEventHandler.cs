using Microsoft.Extensions.Logging;
using Phoenix.Domain.Aggregates.Subscription.Events;

namespace Phoenix.Application.SubscriptionPlan.EventHandlers;

public class SubscriptionPlanDeactivatedEventHandler(ILogger<SubscriptionPlanDeactivatedEventHandler> logger)
    : INotificationHandler<SubscriptionPlanDeactivatedEvent>
{
    public Task Handle(SubscriptionPlanDeactivatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Subscription plan:{planId} deactivated", notification.SubscriptionPlan.Id);
        return Task.CompletedTask;
    }
}