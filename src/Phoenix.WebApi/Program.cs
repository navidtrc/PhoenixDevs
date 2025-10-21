using Framework.EndPoints.Web.Middlewares;
using Phoenix.Infrastructure.Extensions;
using Phoenix.WebApi.WebExtensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

app.UseCustomExceptionHandler();

await app.InitializeDatabaseAsync();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.MapControllers();
app.Run();