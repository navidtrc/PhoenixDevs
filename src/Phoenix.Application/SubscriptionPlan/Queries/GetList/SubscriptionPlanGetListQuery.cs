using Framework.Core.RequestResponse.BasePagination.RequestDtos;
using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Application.SubscriptionPlan.DTOs;

namespace Phoenix.Application.SubscriptionPlan.Queries.GetList;

public class SubscriptionPlanGetListQuery : DataSourceRequest, IRequest<ListResponseBase<SubscriptionPlanDto>>;