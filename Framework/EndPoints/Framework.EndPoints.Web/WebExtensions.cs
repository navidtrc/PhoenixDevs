using Framework.EndPoints.Web.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Framework.EndPoints.Web;

public static class WebExtensions
{
    public static void ConfigureSwaggerGen(this WebApplicationBuilder builder, string appName)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SchemaFilter<UlidSchemaFilter>();
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = @$"{appName} 
                - {System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()!.Location).ProductVersion!.Remove(14)} 
                - {Environment.MachineName}
                - {builder.Environment.EnvironmentName}",
                Version = "v1"
            });
        });
    }
}