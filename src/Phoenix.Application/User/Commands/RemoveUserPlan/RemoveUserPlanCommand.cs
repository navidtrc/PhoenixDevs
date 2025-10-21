using Framework.Core.RequestResponse.BaseResponses;

namespace Phoenix.Application.User.Commands.RemoveUserPlan;

public record RemoveUserPlanCommand(Ulid UserId) : IRequest<ResponseBase>;