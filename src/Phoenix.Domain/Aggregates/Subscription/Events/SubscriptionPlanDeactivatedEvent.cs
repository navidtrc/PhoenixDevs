using Framework.Core.Domain.Abstractions;

namespace Phoenix.Domain.Aggregates.Subscription.Events;

public class SubscriptionPlanDeactivatedEvent(SubscriptionPlan subscriptionPlan) : DomainEvent
{
    public SubscriptionPlan SubscriptionPlan { get; set; } = subscriptionPlan;
}