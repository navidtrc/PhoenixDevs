using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.SubscriptionPlan.Commands.MarkNotReady;

public class MarkNotReadySubscriptionPlanCommandHandler(ApplicationDbContext context)
    : IRequestHandler<MarkNotReadySubscriptionPlanCommand, ResponseBase>
{
    public async Task<ResponseBase> Handle(MarkNotReadySubscriptionPlanCommand request, CancellationToken cancellationToken)
    {
        var subscriptionPlan = await context.SubscriptionPlans.FindAsync(request.PlanId);
        ArgumentNullException.ThrowIfNull(subscriptionPlan);
        subscriptionPlan.MarkNotReady();
        context.SubscriptionPlans.Update(subscriptionPlan);
        await context.SaveChangesAsync(cancellationToken);
        return ResponseBase.Success();
    }
}