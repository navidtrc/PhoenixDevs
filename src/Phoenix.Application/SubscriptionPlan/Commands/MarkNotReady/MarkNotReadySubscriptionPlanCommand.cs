using Framework.Core.RequestResponse.BaseResponses;

namespace Phoenix.Application.SubscriptionPlan.Commands.MarkNotReady;

public record MarkNotReadySubscriptionPlanCommand(Ulid PlanId) : IRequest<ResponseBase>;
