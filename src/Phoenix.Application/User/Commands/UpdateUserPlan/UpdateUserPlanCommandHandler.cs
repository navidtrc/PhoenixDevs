using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.User.Commands.UpdateUserPlan;

public class UpdateUserPlanCommandHandler(ApplicationDbContext context) : IRequestHandler<UpdateUserPlanCommand, ResponseBase>
    
{
    public async Task<ResponseBase> Handle(UpdateUserPlanCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.UserId);
        ArgumentNullException.ThrowIfNull(user);

        var plan = await context.SubscriptionPlans.FindAsync(request.PlanId);
        ArgumentNullException.ThrowIfNull(plan);

        user.UpdatePlan(plan.Id);

        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);

        return ResponseBase.Success();
    }
}