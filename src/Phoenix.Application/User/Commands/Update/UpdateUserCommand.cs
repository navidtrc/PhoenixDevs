using Framework.Core.RequestResponse.BaseResponses;

namespace Phoenix.Application.User.Commands.Update;

public record UpdateUserCommand(Ulid UserId, string Username, string Email) : IRequest<ResponseBase>;