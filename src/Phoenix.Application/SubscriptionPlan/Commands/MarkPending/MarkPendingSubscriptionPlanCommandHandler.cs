using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.SubscriptionPlan.Commands.MarkPending;

public class MarkPendingSubscriptionPlanCommandHandler(ApplicationDbContext context)
    : IRequestHandler<MarkPendingSubscriptionPlanCommand, ResponseBase>
{
    public async Task<ResponseBase> Handle(MarkPendingSubscriptionPlanCommand request, CancellationToken cancellationToken)
    {
        var subscriptionPlan = await context.SubscriptionPlans.FindAsync(request.PlanId);
        ArgumentNullException.ThrowIfNull(subscriptionPlan);
        subscriptionPlan.MarkPending();
        context.SubscriptionPlans.Update(subscriptionPlan);
        await context.SaveChangesAsync(cancellationToken);
        return ResponseBase.Success();
    }
}