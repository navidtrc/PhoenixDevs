using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.User.Commands.Update;

public class UpdateUserCommandHandler(ApplicationDbContext context) : IRequestHandler<UpdateUserCommand, ResponseBase>
{
    public async Task<ResponseBase> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.UserId);
        ArgumentNullException.ThrowIfNull(user);
        user.Update(request.Username, request.Email);
        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);
        return ResponseBase.Success();
    }
}