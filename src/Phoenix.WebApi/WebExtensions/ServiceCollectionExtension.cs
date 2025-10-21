using Framework.Core.Domain.Abstractions;
using Framework.EndPoints.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Phoenix.Application;
using Phoenix.Application.Behavior;
using Phoenix.Domain.Aggregates.User;
using Phoenix.Infrastructure.Contexts;
using Phoenix.Infrastructure.Initializer;
using System.Reflection;

namespace Phoenix.WebApi.WebExtensions;

public static class ServiceCollectionExtension
{
    private static WebApplicationBuilder _builder;
    private static AppSettings _appSettings;

    public static AppSettings AddServices(this WebApplicationBuilder builder)
    {
        _builder = builder;
        ConfigureAppSetting();
        ConfigureServices();
        ConfigureDbContext();
        ConfigureMediatR();
        ConfigureEndpointsService();
        ConfigureCors();
        ConfigureSwaggerGen();
        ConfigureHealthChecks();
        return _appSettings;
    }

    private static void ConfigureAppSetting()
    {
        _builder.Services.Configure<AppSettings>(_builder.Configuration);
        _appSettings = new AppSettings
        {
            ServiceName = _builder.Configuration.GetSection($"{nameof(AppSettings.ServiceName)}").Get<string>()!,
        };

        _builder.Services.AddSingleton(resolver =>
        {
            var appSettings = resolver.GetRequiredService<IOptions<AppSettings>>().Value;
            appSettings = _appSettings;
            return appSettings;
        });
    }

    private static void ConfigureServices()
    {
        _builder.Services.AddHttpContextAccessor();
        _builder.Services.AddControllersWithViews();
        _builder.Services.AddEndpointsApiExplorer();
    }
    

    private static void ConfigureDbContext()
    {
        
        _builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(_builder.Configuration.GetConnectionString("DefaultConnection"), db =>
            {
                db.MigrationsHistoryTable(ApplicationDbContextSchema.EF_MIGRATION_TABLE_NAME,
                    _appSettings.ServiceName);
                db.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);
                db.EnableRetryOnFailure();
                db.MigrationsAssembly(typeof(ApplicationDbContextSchema).Assembly.FullName);
            });
        });
        _builder.Services.AddDbContext<ApplicationDbContextReadOnly>(options =>
        {
            options.UseSqlServer(_builder.Configuration.GetConnectionString("ReadOnlyConnection"), db =>
            {
                db.MigrationsHistoryTable(ApplicationDbContextSchema.EF_MIGRATION_TABLE_NAME,
                    _appSettings.ServiceName);
                db.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);
                db.EnableRetryOnFailure();
                db.MigrationsAssembly(typeof(ApplicationDbContextSchema).Assembly.FullName);
            }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        _builder.Services.AddScoped<ApplicationDbContextInitializer>();
        
    }

    private static void ConfigureMediatR()
    {
        _builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), ServiceAssemblies.WEB_ASSEMBLY);
        });
        _builder.Services.AddScoped<IDomainEventDispatcher, EventDispatcher>();
    }

    private static void ConfigureEndpointsService()
    {
        _builder.Services.AddEndpointsApiExplorer();
    }

    private static void ConfigureHealthChecks()
    {
        _builder.Services.AddHealthChecks();
    }
    private static void ConfigureCors()
    {
        _builder.Services.AddCors(options =>
        {
            var allowOrigins = _builder.Configuration.GetSection(nameof(AppSettings.AllowOrigins)).Get<string[]>();
            options.AddDefaultPolicy(builder => builder
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .WithOrigins(allowOrigins!)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithExposedHeaders("Access-Control-Allow-Origin", "Access-Control-Allow-Credentials", "X-Pagination", "Otp-Code")
            );
        });
    }
    private static void ConfigureSwaggerGen()
    {
        _builder.ConfigureSwaggerGen(_appSettings.ServiceName);
    }
}

public static class ServiceAssemblies
{
    public static readonly Assembly WEB_ASSEMBLY = typeof(Program).Assembly;
    public static readonly Assembly APPLICATION_ASSEMBLY = typeof(AppSettings).Assembly;
    public static readonly Assembly INFRA_ASSEMBLY = typeof(ApplicationDbContext).Assembly;
    public static readonly Assembly DOMAIN_ASSEMBLY = typeof(User).Assembly;
}