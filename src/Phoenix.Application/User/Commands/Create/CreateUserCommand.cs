using Framework.Core.RequestResponse.BaseResponses;

namespace Phoenix.Application.User.Commands.Create;

public record CreateUserCommand(string Username, string Email) : IRequest<ResponseBase>;