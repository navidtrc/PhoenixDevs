using Microsoft.Extensions.Logging;
using Phoenix.Domain.Aggregates.Subscription.Events;

namespace Phoenix.Application.SubscriptionPlan.EventHandlers;

public class SubscriptionPlanActivatedEventHandler(ILogger<SubscriptionPlanActivatedEventHandler> logger)
    : INotificationHandler<SubscriptionPlanActivatedEvent>
{
    public Task Handle(SubscriptionPlanActivatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Subscription plan:{planId} activated", notification.SubscriptionPlan.Id);
        return Task.CompletedTask;
    }
}