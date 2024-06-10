using BnFurniture.Domain.Responses;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;

namespace BnFurnitureAdmin.Middleware;

public class LogAndExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogAndExceptionHandlerMiddleware> _logger;

    public LogAndExceptionHandlerMiddleware(RequestDelegate next, ILogger<LogAndExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestName = context.Request.Path.Value;
        var requestMethod = context.Request.Method;
        var requestGuid = Guid.NewGuid().ToString();
        var requestFullString = $"({requestMethod}) ({requestName}) ({requestGuid})";

        _logger.LogInformation($"[START] {requestFullString}");

        try
        {
            // Check if the request contains multipart/form-data
            if (context.Request.ContentType != null && context.Request.ContentType.Contains("multipart/form-data"))
            {
                context.Request.EnableBuffering();

                var memoryStream = new MemoryStream();
                await context.Request.Body.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                context.Request.Body = memoryStream; // Reset the stream for further processing

                var form = await context.Request.ReadFormAsync();
                foreach (var field in form)
                {
                    if (!field.Key.Equals("file", StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.LogInformation($"Form Field: {field.Key} = {field.Value}");
                    }
                }

                memoryStream.Position = 0; // Reset again for the next middleware
            }

            await _next(context);
        }
        catch (MySqlException ex)
        {
            _logger.LogError(ex, $"[ERROR] {requestFullString} {Environment.NewLine}{ex.GetType().Name}: {ex.Message}");
            await HandleExceptionAsync(context, ex, (int)HttpStatusCode.ServiceUnavailable);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, $"[ERROR] {requestFullString} {Environment.NewLine}{ex.GetType().Name}: {ex.Message}");
            await HandleExceptionAsync(context, ex, (int)HttpStatusCode.UnprocessableEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[ERROR] {requestFullString} {Environment.NewLine}{ex.GetType().Name}: {ex.Message}");
            await HandleExceptionAsync(context, ex);
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation($"[END] {requestFullString}; {stopwatch.ElapsedMilliseconds}ms");
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception,
        int statusCode = (int)HttpStatusCode.InternalServerError)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var responseModel = new ApiBaseResponse(false, statusCode)
        {
            Message = $"{exception.GetType().Name}: {exception.Message}"
        };

        await context.Response.WriteAsJsonAsync(responseModel);
    }
}