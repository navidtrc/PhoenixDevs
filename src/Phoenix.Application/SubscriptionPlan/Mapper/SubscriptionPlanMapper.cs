using Phoenix.Application.SubscriptionPlan.DTOs;

namespace Phoenix.Application.SubscriptionPlan.Mapper;

public static class SubscriptionPlanMapper
{
    public static SubscriptionPlanDto ToDto(this Domain.Aggregates.Subscription.SubscriptionPlan plan)
    {
        if (plan is null) throw new ArgumentNullException(nameof(plan));

        return new SubscriptionPlanDto
        {
            Id = plan.Id,
            Title = plan.Title,
            Description = plan.Description,
            Duration = plan.Duration,
            Price = plan.Price.Amount,
            Status = plan.Status,
        };
    }

    public static List<SubscriptionPlanDto> ToDtoList(this IEnumerable<Domain.Aggregates.Subscription.SubscriptionPlan> plans)
        => plans.Select(ToDto).ToList();
}