using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Domain.Aggregates.Subscription.Enums;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.User.Commands.ActivateReservedPlan;

public class ActivateReservedPlanCommandHandler(ApplicationDbContext context) : IRequestHandler<ActivateReservedPlanCommand, ResponseBase>
{
    public async Task<ResponseBase> Handle(ActivateReservedPlanCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.UserId);
        ArgumentNullException.ThrowIfNull(user);
        
        if (user.ReservedPlanId is null)
            throw new InvalidOperationException("No reserved plan to activate.");
        
        if (user.CurrentPlan is not null &&
            user.CurrentPlan.Status != SubscriptionPlanStatus.Inactive)
            throw new InvalidOperationException("Current plan is active; cannot activate reserved.");

        user.ActivateReservedPlan();
        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);
        return ResponseBase.Success();
    }
}