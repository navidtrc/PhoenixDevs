using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phoenix.Infrastructure.Contexts;

namespace Phoenix.Infrastructure.Initializer;

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext context)
{
    public async Task InitializeAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}