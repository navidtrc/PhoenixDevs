using Framework.Core.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Phoenix.Application.Behavior;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (ValidationException ve)
        {
            string requestName = typeof(TRequest).Name;
            _logger.LogError(ve, "Profile Request: Validation exception for request {Name} {@Request}", requestName,
                request);

            string validationErrors = JsonConvert.SerializeObject(ve.Errors);
            throw new DomainException(HttpStatusCode.BadRequest, validationErrors);
        }
        catch (DomainException e)
        {
            string requestName = typeof(TRequest).Name;
            _logger.LogError(e, "Profile Request: Domain exception for request {Name} {@Request}", requestName,
                request);
            
            throw;
        }
        catch (Exception ex)
        {
            string? requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "Profile Request: Unhandled Exceptions for Request {Name} {@Request}", requestName, request);
            _logger.LogError(ex.Message);
            _logger.LogError(ex.InnerException?.Message);
            
            throw;
        }
    }
}
