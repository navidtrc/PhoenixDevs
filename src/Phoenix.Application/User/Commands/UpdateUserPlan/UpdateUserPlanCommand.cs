using Framework.Core.RequestResponse.BaseResponses;

namespace Phoenix.Application.User.Commands.UpdateUserPlan;

public record UpdateUserPlanCommand(Ulid UserId, Ulid PlanId) : IRequest<ResponseBase>;