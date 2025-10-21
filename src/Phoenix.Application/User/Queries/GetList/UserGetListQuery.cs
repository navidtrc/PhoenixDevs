using Framework.Core.RequestResponse.BasePagination.RequestDtos;
using Framework.Core.RequestResponse.BaseResponses;
using Phoenix.Application.User.DTOs;

namespace Phoenix.Application.User.Queries.GetList;

public class UserGetListQuery : DataSourceRequest, IRequest<ListResponseBase<UserDto>>;