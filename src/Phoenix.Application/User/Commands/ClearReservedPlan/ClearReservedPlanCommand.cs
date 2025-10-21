using Framework.Core.RequestResponse.BaseResponses;

namespace Phoenix.Application.User.Commands.ClearReservedPlan;

public record ClearReservedPlanCommand(Ulid UserId) : IRequest<ResponseBase>;
