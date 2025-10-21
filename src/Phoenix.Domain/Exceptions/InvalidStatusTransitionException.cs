using Framework.Core.Domain.Exceptions;
using Phoenix.Domain.Aggregates.Subscription.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Phoenix.Domain.Exceptions;

public class InvalidStatusTransitionException : DomainException
{
    public InvalidStatusTransitionException(SubscriptionPlanStatus current, SubscriptionPlanStatus target)
        : base($"Invalid status transition from '{current}' to '{target}'.")
    {
    }

    [DoesNotReturn]
    public static void Throw(SubscriptionPlanStatus current, SubscriptionPlanStatus target)
    {
        throw new InvalidStatusTransitionException(current, target);
    }
}