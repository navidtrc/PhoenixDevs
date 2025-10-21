using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Application.SubscriptionPlan.DTOs;

namespace Phoenix.Application.SubscriptionPlan.Queries.GetById;

public record SubscriptionPlanGetByIdQuery(Ulid PlanId) : IRequest<ResponseBase<SubscriptionPlanDto>>;