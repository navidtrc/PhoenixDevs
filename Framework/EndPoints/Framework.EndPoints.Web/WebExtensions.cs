using Asp.Versioning;
using Framework.EndPoints.Web.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Framework.EndPoints.Web;

public static class WebExtensions
{
    public static void ConfigureSerilog(this WebApplicationBuilder builder,
        string appName,
        string abbr)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information() 
            .Enrich.FromLogContext()
            .Enrich.WithClientIp()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithProcessId()
            .Enrich.WithProcessName()
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .Enrich.WithCorrelationId()
            .Enrich.WithProperty("Application", appName)
            .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
            .Enrich.WithProperty("Service", abbr)
            .Destructure.ToMaximumDepth(4)
            .Destructure.ToMaximumStringLength(100)
            .Destructure.ToMaximumCollectionCount(10)
            .WriteTo.Console(
                outputTemplate:
                "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} | {ClientIp} | {CorrelationId} | [{Level:u4}] | {Message:lj}{NewLine}{Exception}")
            .WriteTo.File($"logs/{builder.Environment.EnvironmentName}_{abbr}_.log",
                rollingInterval:
                RollingInterval.Hour,
                rollOnFileSizeLimit: true,
                outputTemplate:
                "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} | {ClientIp} | {CorrelationId} | [{Level:u4}] | {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        builder.Host.UseSerilog(Log.Logger);
    }

    public static void ConfigureSwaggerGen(this WebApplicationBuilder builder, string appName, string abbr)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(x => x.FullName);

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = @$"{appName} 
                - {System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()!.Location).ProductVersion!.Remove(14)} 
                - {Environment.MachineName}
                - {builder.Environment.EnvironmentName}",
                Version = "v1"
            });

            if (!builder.Environment.IsDevelopment()) c.DocumentFilter<PathPrefixInsertDocumentFilter>($"/{abbr}");

            c.OperationFilter<RemoveVersionParameters>();

            c.DocumentFilter<SetVersionInPaths>();

            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                var versions = methodInfo.DeclaringType
                    .GetCustomAttributes<ApiVersionAttribute>(true)
                    .SelectMany(attr => attr.Versions);

                return versions.Any(v => $"v{v}" == docName);
            });
        });
    }

}

