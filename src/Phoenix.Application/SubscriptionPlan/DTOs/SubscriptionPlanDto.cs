using Phoenix.Domain.Aggregates.Subscription.Enums;

namespace Phoenix.Application.SubscriptionPlan.DTOs;

public class SubscriptionPlanDto
{
    public Ulid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Duration { get; set; }
    public SubscriptionPlanStatus Status { get; set; }
}