using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.User.Commands.RemoveUserPlan;

public class RemoveUserPlanCommandHandler(ApplicationDbContext context) : IRequestHandler<RemoveUserPlanCommand, ResponseBase>
{
    public async Task<ResponseBase> Handle(RemoveUserPlanCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.UserId);
        ArgumentNullException.ThrowIfNull(user);
        user.ActivateReservedPlan();
        user.RemoveCurrentPlan();
        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);
        return ResponseBase.Success();
    }
}