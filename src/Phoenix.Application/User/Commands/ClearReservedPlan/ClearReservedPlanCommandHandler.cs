using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.User.Commands.ClearReservedPlan;

public class ClearReservedPlanCommandHandler(ApplicationDbContext context) : IRequestHandler<ClearReservedPlanCommand, ResponseBase>
{
    public async Task<ResponseBase> Handle(ClearReservedPlanCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.UserId);
        ArgumentNullException.ThrowIfNull(user);
        user.ClearReservedPlan();
        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);
        return ResponseBase.Success();
    }
}