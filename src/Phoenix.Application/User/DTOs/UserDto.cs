namespace Phoenix.Application.User.DTOs;

public class UserDto
{
    public Ulid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public Ulid? CurrentPlanId { get; set; }
    public Ulid? ReservedPlanId { get; set; }
}