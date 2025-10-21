using Framework.Core.Domain.Abstractions;

namespace Phoenix.Domain.Aggregates.Subscription.Events;

public class SubscriptionPlanPendingEvent(SubscriptionPlan subscriptionPlan) : DomainEvent
{
    public SubscriptionPlan SubscriptionPlan { get; set; } = subscriptionPlan;
}