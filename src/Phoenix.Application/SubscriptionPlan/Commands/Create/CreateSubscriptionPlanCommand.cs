using Framework.Core.RequestResponse.BaseResponses;

namespace Phoenix.Application.SubscriptionPlan.Commands.Create;

public record CreateSubscriptionPlanCommand(
    string Title,
    string Description,
    decimal Price,
    int Duration) : IRequest<ResponseBase>;