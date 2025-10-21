using Framework.Core.RequestResponse.BaseResponses;

namespace Phoenix.Application.SubscriptionPlan.Commands.Update;

public class UpdateSubscriptionPlanCommand : IRequest<ResponseBase>
{
    public Ulid PlanId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? Duration { get; set; }
}