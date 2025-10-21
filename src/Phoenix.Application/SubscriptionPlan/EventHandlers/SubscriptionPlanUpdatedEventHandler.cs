using Microsoft.Extensions.Logging;
using Phoenix.Domain.Aggregates.Subscription.Events;

namespace Phoenix.Application.SubscriptionPlan.EventHandlers;

public class SubscriptionPlanUpdatedEventHandler(ILogger<SubscriptionPlanUpdatedEventHandler> logger)
    : INotificationHandler<SubscriptionPlanUpdatedEvent>
{
    public Task Handle(SubscriptionPlanUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Subscription plan updated");
        return Task.CompletedTask;
    }
}