using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.User.Commands.SetUserReservedPlan;

public class SetUserReservedPlanCommandHandler(ApplicationDbContext context) : IRequestHandler<SetUserReservedPlanCommand, ResponseBase>
{
    public async Task<ResponseBase> Handle(SetUserReservedPlanCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.UserId);
        ArgumentNullException.ThrowIfNull(user);

        var reservedPlan = await context.SubscriptionPlans.FindAsync(request.ReservedPlanId);
        ArgumentNullException.ThrowIfNull(reservedPlan);
        
        user.SetReservedPlan(reservedPlan);
        
        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);

        return ResponseBase.Success();
    }
}