using Phoenix.Domain.Aggregates.Subscription.Enums;
using Phoenix.Domain.Aggregates.Subscription.Events;

namespace Phoenix.Domain.Aggregates.Subscription;

public class SubscriptionPlan : AggregateRoot
{
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public Price Price { get; private set; } = null!;
    public int Duration { get; private set; }
    public SubscriptionPlanStatus Status { get; private set; }

    private SubscriptionPlan()
    {
        Status = SubscriptionPlanStatus.Active;
    }

    public static SubscriptionPlan Create(string title, string description, decimal price, int duration)
    {
        ValidateValues(
            title: title,
            description: description,
            price: price,
            duration: duration
        );

        var plan = new SubscriptionPlan
        {
            Title = title.Trim(),
            Description = string.IsNullOrWhiteSpace(description) ? null : description!.Trim(),
            Price = price,
            Duration = duration
        };

        plan.AddDomainEvent(new SubscriptionPlanCreatedEvent(plan));
        return plan;
    }


    public void Update(string? title = null,
        string? description = null,
        decimal? price = null,
        int? duration = null
    )
    {
        if (title is null && description is null && price is null && duration is null)
            return;

        ValidateValues(
            title: title,
            description: description,
            price: price,
            duration: duration
        );
        var changed = false;

        if (title is not null)
        {
            var normalized = title.Trim();
            if (!string.Equals(Title, normalized, StringComparison.InvariantCulture))
            {
                Title = normalized;
                changed = true;
            }
        }

        if (description is not null)
        {
            var normalized = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
            if (!string.Equals(Description, normalized, StringComparison.InvariantCulture))
            {
                Description = normalized;
                changed = true;
            }
        }

        if (price.HasValue)
        {
            var newPrice = (Price)price.Value;
            if (!Price.Equals(newPrice))
            {
                Price = newPrice;
                changed = true;
            }
        }

        if (duration.HasValue && Duration != duration.Value)
        {
            Duration = duration.Value;
            changed = true;
        }

        if (changed)
        {
            AddDomainEvent(new SubscriptionPlanUpdatedEvent(this));
        }

    }
    private static void ValidateValues(
        string? title = null,
        string? description = null,
        decimal? price = null,
        int? duration = null)
    {
        if (title is not null)
        {
            Guard.Against.NullOrWhiteSpace(
                title,
                nameof(title),
                exceptionCreator: () => new RequiredException(nameof(title)));

            Guard.Against.LengthOutOfRange(
                title,
                Consts.SUBSCRIPTION_PLAN_TITLE.MinLength,
                Consts.SUBSCRIPTION_PLAN_TITLE.MaxLength,
                exceptionCreator: () => new StringLengthBetweenException(
                    nameof(title),
                    Consts.SUBSCRIPTION_PLAN_TITLE.MinLength,
                    Consts.SUBSCRIPTION_PLAN_TITLE.MaxLength));
        }
        if (description is not null)
        {
            if (!string.IsNullOrWhiteSpace(description))
            {
                Guard.Against.LengthOutOfRange(
                    description,
                    Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MinLength,
                    Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MaxLength,
                    exceptionCreator: () => new StringLengthBetweenException(
                        nameof(description),
                        Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MinLength,
                        Consts.SUBSCRIPTION_PLAN_DESCRIPTION.MaxLength));
            }
        }

        if (price.HasValue)
        {
            Guard.Against.OutOfRange(
                price.Value,
                nameof(price),
                Consts.SUBSCRIPTION_PLAN_PRICE.MinValue,
                Consts.SUBSCRIPTION_PLAN_PRICE.MaxValue,
                exceptionCreator: () =>
                {
                    ValueBetweenException.Throw(nameof(price),
                        Consts.SUBSCRIPTION_PLAN_PRICE.MinValue,
                        Consts.SUBSCRIPTION_PLAN_PRICE.MaxValue);
                    return null!;
                });
        }

        if (duration.HasValue)
        {
            Guard.Against.OutOfRange(
                duration.Value,
                nameof(duration),
                Consts.SUBSCRIPTION_PLAN_DURATION.MinValue,
                Consts.SUBSCRIPTION_PLAN_DURATION.MaxValue,
                exceptionCreator: () =>
                {
                    ValueBetweenException.Throw(nameof(duration),
                        Consts.SUBSCRIPTION_PLAN_DURATION.MinValue,
                        Consts.SUBSCRIPTION_PLAN_DURATION.MaxValue);
                    return null!;
                });
        }
    }

    public void Activate()
    {
        if (Status == SubscriptionPlanStatus.Active)
            return;

        if (Status == SubscriptionPlanStatus.NotReady || Status == SubscriptionPlanStatus.Pending || Status == SubscriptionPlanStatus.Inactive)
        {
            Status = SubscriptionPlanStatus.Active;
            AddDomainEvent(new SubscriptionPlanActivatedEvent(this));
        }
        else
        {
            InvalidStatusTransitionException.Throw(Status, SubscriptionPlanStatus.Active);
        }
    }

    public void Deactivate()
    {
        if (Status == SubscriptionPlanStatus.Inactive)
            return; 

        if (Status == SubscriptionPlanStatus.Active || Status == SubscriptionPlanStatus.Pending)
        {
            Status = SubscriptionPlanStatus.Inactive;
            AddDomainEvent(new SubscriptionPlanDeactivatedEvent(this));
        }
        else
        {
            InvalidStatusTransitionException.Throw(Status, SubscriptionPlanStatus.Inactive);
        }
    }

    public void MarkPending()
    {
        if (Status == SubscriptionPlanStatus.Pending)
            return; 

        if (Status == SubscriptionPlanStatus.NotReady)
        {
            Status = SubscriptionPlanStatus.Pending;
            AddDomainEvent(new SubscriptionPlanPendingEvent(this));
        }
        else
        {
            InvalidStatusTransitionException.Throw(Status, SubscriptionPlanStatus.Pending);
        }
    }

    public void MarkNotReady()
    {
        if (Status == SubscriptionPlanStatus.NotReady)
            return; 

        if (Status == SubscriptionPlanStatus.Pending || Status == SubscriptionPlanStatus.Inactive)
        {
            Status = SubscriptionPlanStatus.NotReady;
            AddDomainEvent(new SubscriptionPlanMarkedNotReadyEvent(this));
        }
        else
        {
            InvalidStatusTransitionException.Throw(Status, SubscriptionPlanStatus.NotReady);
        }
    }
}