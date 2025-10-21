using Framework.Core.RequestResponse.BaseResponses;
using Microsoft.EntityFrameworkCore;
using Phoenix.Application.User.DTOs;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Application.User.Queries.GetById;

public class UserGetByIdQueryHandler(ApplicationDbContextReadOnly context) : IRequestHandler<UserGetByIdQuery, ResponseBase<UserDto>>
{
    public async Task<ResponseBase<UserDto>> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
    {
        var planDto = await context.Users.AsNoTracking()
            .Select(s => new UserDto
            {
                Id = s.Id,
                Username = s.Username,
                Email = s.Email.ToString(),
                CurrentPlanId = s.CurrentPlanId,
                ReservedPlanId = s.ReservedPlanId,
            }).SingleOrDefaultAsync(f => f.Id == request.UserId, cancellationToken);
        if (planDto == null) return ResponseStatus.NotFound;
        return planDto;
    }
}