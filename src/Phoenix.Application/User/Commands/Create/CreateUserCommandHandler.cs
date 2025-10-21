using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.User.Commands.Create;

public class CreateUserCommandHandler(ApplicationDbContext context) : IRequestHandler<CreateUserCommand, ResponseBase>
{
    public async Task<ResponseBase> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = Domain.Aggregates.User.User.Create(request.Username, request.Email);
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return ResponseBase.Success();
    }
}