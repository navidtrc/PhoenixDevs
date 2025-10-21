using Framework.Core.RequestResponse.BasePagination;
using Framework.Core.RequestResponse.BaseResponses;
using Microsoft.EntityFrameworkCore;
using Phoenix.Application.SubscriptionPlan.DTOs;
using Phoenix.Application.SubscriptionPlan.Mapper;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.SubscriptionPlan.Queries.GetList;

public class SubscriptionPlanGetListQueryHandler(ApplicationDbContextReadOnly context)
    : IRequestHandler<SubscriptionPlanGetListQuery, ListResponseBase<SubscriptionPlanDto>>
{
    public async Task<ListResponseBase<SubscriptionPlanDto>> Handle(SubscriptionPlanGetListQuery request, CancellationToken cancellationToken)
    {
        var result = await context.SubscriptionPlans.AsNoTracking()
            .ToDataSourceResultAsync(request, cancellationToken);
        if (!result.Data.Any())
            return ResponseStatus.NotFound;
        return ListResponseBase<SubscriptionPlanDto>.Success(result.Data.ToDtoList());
    }
}