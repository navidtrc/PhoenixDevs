using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.SubscriptionPlan.Commands.Update;

public class UpdateSubscriptionPlanCommandHandler(ApplicationDbContext context)
    : IRequestHandler<UpdateSubscriptionPlanCommand, ResponseBase>
{
    public async Task<ResponseBase> Handle(UpdateSubscriptionPlanCommand request, CancellationToken cancellationToken)
    {
        var subscriptionPlan = await context.SubscriptionPlans.FindAsync(request.PlanId);
        ArgumentNullException.ThrowIfNull(subscriptionPlan);
        subscriptionPlan.Update(request.Title, request.Description, request.Price, request.Duration);
        context.SubscriptionPlans.Update(subscriptionPlan);
        await context.SaveChangesAsync(cancellationToken);
        return ResponseBase.Success();
    }
}