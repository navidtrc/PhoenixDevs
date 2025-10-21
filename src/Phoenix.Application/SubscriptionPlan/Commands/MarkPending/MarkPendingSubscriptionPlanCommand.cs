using Framework.Core.RequestResponse.BaseResponses;

namespace Phoenix.Application.SubscriptionPlan.Commands.MarkPending;

public record MarkPendingSubscriptionPlanCommand(Ulid PlanId) : IRequest<ResponseBase>;
