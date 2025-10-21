using Framework.Core.RequestResponse.BaseResponses;

namespace Phoenix.Application.User.Commands.ActivateReservedPlan;

public record ActivateReservedPlanCommand(Ulid UserId) : IRequest<ResponseBase>;
