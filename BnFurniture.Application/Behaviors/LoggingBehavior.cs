using Mediator;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json;

namespace BnFurniture.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async ValueTask<TResponse> Handle(
        TRequest request, 
        CancellationToken cancellationToken, 
        MessageHandlerDelegate<TRequest, TResponse> next)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestName = request.GetType().Name;
        var requestGuid = Guid.NewGuid().ToString();
        var requestNameWithGuid = $"{requestName} [{requestGuid}]";
        TResponse response;

        _logger.LogInformation($"[START] {requestNameWithGuid}");

        try
        {
            try
            {
                _logger.LogInformation($"[PROPS] {requestNameWithGuid} {JsonSerializer.Serialize(request)}");
            }
            catch (NotSupportedException)
            {
                _logger.LogError($"[ERROR - SERIALIZATION] {requestNameWithGuid} - Could not serialize the request.");
            }

            // Pre logic
            response = await next(request, cancellationToken);
            // Post Logic
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, $"[ERROR - VALIDATION] {requestNameWithGuid} - {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[ERROR] {requestNameWithGuid} - {ex.Message}");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation(
                $"[END] {requestNameWithGuid}; {stopwatch.ElapsedMilliseconds}ms");
        }

        return response;
    }
}

/* MediatR
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestName = request.GetType().Name;
        var requestGuid = Guid.NewGuid().ToString();
        var requestNameWithGuid = $"{requestName} [{requestGuid}]";
        TResponse response;

        _logger.LogInformation($"[START] {requestNameWithGuid}");

        try
        {
            try
            {
                _logger.LogInformation($"[PROPS] {requestNameWithGuid} {JsonSerializer.Serialize(request)}");
            }
            catch (NotSupportedException)
            {
                _logger.LogError($"[ERROR - SERIALIZATION] {requestNameWithGuid} - Could not serialize the request.");
            }

            // Pre logic
            response = await next();
            // Post Logic
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, $"[ERROR - VALIDATION] {requestNameWithGuid} - {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[ERROR] {requestNameWithGuid} - {ex.Message}");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation(
                $"[END] {requestNameWithGuid}; {stopwatch.ElapsedMilliseconds}ms");
        }

        return response;
    }
*/