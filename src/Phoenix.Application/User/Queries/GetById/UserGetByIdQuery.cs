using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Application.User.DTOs;

namespace Phoenix.Application.User.Queries.GetById;

public record UserGetByIdQuery(Ulid UserId) : IRequest<ResponseBase<UserDto>>;