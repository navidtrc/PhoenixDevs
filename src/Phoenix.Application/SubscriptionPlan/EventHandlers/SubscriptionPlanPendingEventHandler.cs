using Microsoft.Extensions.Logging;
using Phoenix.Domain.Aggregates.Subscription.Events;

namespace Phoenix.Application.SubscriptionPlan.EventHandlers;

public class SubscriptionPlanPendingEventHandler(ILogger<SubscriptionPlanPendingEventHandler> logger)
    : INotificationHandler<SubscriptionPlanPendingEvent>
{
    public Task Handle(SubscriptionPlanPendingEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Subscription plan:{planId} pending", notification.SubscriptionPlan.Id);
        return Task.CompletedTask;
    }
}