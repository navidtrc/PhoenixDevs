using Microsoft.Extensions.Logging;
using Phoenix.Domain.Aggregates.Subscription.Events;

namespace Phoenix.Application.SubscriptionPlan.EventHandlers;

public class SubscriptionPlanCreatedEventHandler(ILogger<SubscriptionPlanCreatedEventHandler> logger)
    : INotificationHandler<SubscriptionPlanCreatedEvent>
{
    public Task Handle(SubscriptionPlanCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Subscription plan:{planId} created", notification.SubscriptionPlan.Id);
        return Task.CompletedTask;
    }
}