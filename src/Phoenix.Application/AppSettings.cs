namespace Phoenix.Application;

public class AppSettings
{
    public required string ServiceName { get; init; }
    public string[] AllowOrigins { get; init; } = [];
}