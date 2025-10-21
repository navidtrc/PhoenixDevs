using Framework.EndPoints.Web.Middlewares;
using Phoenix.Infrastructure.Extensions;
using Phoenix.WebApi.WebExtensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

app.UseCustomExceptionHandler();

await app.InitializeDatabaseAsync();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();