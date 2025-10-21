using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.SubscriptionPlan.Commands.Activate;

public class ActivateSubscriptionPlanCommandHandler(ApplicationDbContext context)
    : IRequestHandler<ActivateSubscriptionPlanCommand, ResponseBase>
{
    public async Task<ResponseBase> Handle(ActivateSubscriptionPlanCommand request, CancellationToken cancellationToken)
    {
        var subscriptionPlan = await context.SubscriptionPlans.FindAsync(request.PlanId);
        ArgumentNullException.ThrowIfNull(subscriptionPlan);
        subscriptionPlan.Activate();
        context.SubscriptionPlans.Update(subscriptionPlan);
        await context.SaveChangesAsync(cancellationToken);
        return ResponseBase.Success();
    }
}