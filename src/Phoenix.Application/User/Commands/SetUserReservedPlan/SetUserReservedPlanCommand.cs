using Framework.Core.RequestResponse.BaseResponses;

namespace Phoenix.Application.User.Commands.SetUserReservedPlan;

public record SetUserReservedPlanCommand(Ulid UserId, Ulid ReservedPlanId) : IRequest<ResponseBase>;
