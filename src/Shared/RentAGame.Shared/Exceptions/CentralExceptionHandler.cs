using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RentAGame.Shared.Exceptions;

public class CentralExceptionHandler(ILogger<CentralExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Error : {exceptionMessage} : {time}", exception.Message, DateTime.UtcNow);
        var title = string.Empty;
        string detail = string.Empty;
        int statusCode = StatusCodes.Status500InternalServerError;

        switch (exception)
        {
            case ValidationException:
            case BadRequestException:
                title = exception.GetType().Name;
                detail = exception.Message;
                statusCode = StatusCodes.Status400BadRequest;
                break;
            case NotFoundException:
                title = exception.GetType().Name;
                detail = exception.Message;
                statusCode = StatusCodes.Status404NotFound;
                break;
            default:
                title = exception.GetType().Name;
                detail = exception.Message;
                statusCode = StatusCodes.Status500InternalServerError;
                break;
        }
        httpContext.Response.StatusCode = statusCode;

        var problemDetails = new ProblemDetails
        {
            Title = string.Empty,
            Detail = detail,
            Status = statusCode,
            Instance = httpContext.Request.Path
        };
        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}