using Framework.Core.RequestResponse.BasePagination;
using Framework.Core.RequestResponse.BaseResponses;
using Microsoft.EntityFrameworkCore;
using Phoenix.Application.User.DTOs;
using Phoenix.Application.User.Mapper;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.User.Queries.GetList;

public class UserGetListQueryHandler(ApplicationDbContextReadOnly context)
    : IRequestHandler<UserGetListQuery, ListResponseBase<UserDto>>
{
    public async Task<ListResponseBase<UserDto>> Handle(UserGetListQuery request, CancellationToken cancellationToken)
    {
        var result = await context.Users.AsNoTracking()
            .ToDataSourceResultAsync(request, cancellationToken);
        if (!result.Data.Any())
            return ResponseStatus.NotFound;
        return ListResponseBase<UserDto>.Success(result.Data.ToDtoList());
    }
}