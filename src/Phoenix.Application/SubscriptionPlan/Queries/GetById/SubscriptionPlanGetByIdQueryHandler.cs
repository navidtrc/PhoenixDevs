using Framework.Core.RequestResponse.BaseResponses;
using Microsoft.EntityFrameworkCore;
using Phoenix.Application.SubscriptionPlan.DTOs;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.SubscriptionPlan.Queries.GetById;

public class SubscriptionPlanGetByIdQueryHandler(ApplicationDbContextReadOnly context) : IRequestHandler<SubscriptionPlanGetByIdQuery, ResponseBase<SubscriptionPlanDto>>
{
    public async Task<ResponseBase<SubscriptionPlanDto>> Handle(SubscriptionPlanGetByIdQuery request, CancellationToken cancellationToken)
    {
        var planDto = await context.SubscriptionPlans
            .AsNoTracking()
            .Select(s => new SubscriptionPlanDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Duration = s.Duration,
                Status = s.Status,
            }).SingleOrDefaultAsync(f => f.Id == request.PlanId, cancellationToken);
        if (planDto == null) return ResponseStatus.NotFound;
        return planDto;
    }
}