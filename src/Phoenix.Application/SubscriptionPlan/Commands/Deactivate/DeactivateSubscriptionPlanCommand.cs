using Framework.Core.RequestResponse.BaseResponses;

namespace Phoenix.Application.SubscriptionPlan.Commands.Deactivate;

public record DeactivateSubscriptionPlanCommand(Ulid PlanId) : IRequest<ResponseBase>;
