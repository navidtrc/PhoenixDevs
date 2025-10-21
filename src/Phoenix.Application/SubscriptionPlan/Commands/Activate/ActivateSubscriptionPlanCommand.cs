using Framework.Core.RequestResponse.BaseResponses;

namespace Phoenix.Application.SubscriptionPlan.Commands.Activate;

public record ActivateSubscriptionPlanCommand(Ulid PlanId) : IRequest<ResponseBase>;
