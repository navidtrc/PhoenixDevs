using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.SubscriptionPlan.Commands.Deactivate;

public class DeactivateSubscriptionPlanCommandHandler(ApplicationDbContext context)
    : IRequestHandler<DeactivateSubscriptionPlanCommand, ResponseBase>
{
    public async Task<ResponseBase> Handle(DeactivateSubscriptionPlanCommand request, CancellationToken cancellationToken)
    {
        var subscriptionPlan = await context.SubscriptionPlans.FindAsync(request.PlanId);
        ArgumentNullException.ThrowIfNull(subscriptionPlan);
        subscriptionPlan.Deactivate();
        context.SubscriptionPlans.Update(subscriptionPlan);
        await context.SaveChangesAsync(cancellationToken);
        return ResponseBase.Success();
    }
}