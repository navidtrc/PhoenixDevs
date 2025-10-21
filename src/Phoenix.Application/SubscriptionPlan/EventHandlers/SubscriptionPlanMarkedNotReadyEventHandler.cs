using Microsoft.Extensions.Logging;
using Phoenix.Domain.Aggregates.Subscription.Events;

namespace Phoenix.Application.SubscriptionPlan.EventHandlers;

public class SubscriptionPlanMarkedNotReadyEventHandler(ILogger<SubscriptionPlanMarkedNotReadyEventHandler> logger)
    : INotificationHandler<SubscriptionPlanMarkedNotReadyEvent>
{
    public Task Handle(SubscriptionPlanMarkedNotReadyEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Subscription plan:{planId} marked not ready", notification.SubscriptionPlan.Id);
        return Task.CompletedTask;
    }
}