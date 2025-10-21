using Phoenix.Application.User.DTOs;

namespace Phoenix.Application.User.Mapper;

public static class UserMapper
{
    public static UserDto ToDto(this Domain.Aggregates.User.User user)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email.ToString(),
            Username = user.Username,
            CurrentPlanId = user.CurrentPlanId,
            ReservedPlanId = user.ReservedPlanId,
        };
    }

    public static List<UserDto> ToDtoList(this IEnumerable<Domain.Aggregates.User.User> users)
        => users.Select(ToDto).ToList();
}