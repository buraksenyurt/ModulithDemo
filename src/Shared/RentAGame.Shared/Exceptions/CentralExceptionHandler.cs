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

        var problemDetails = new ProblemDetails
        {
            Title = string.Empty,
            Detail = string.Empty,
            Status = StatusCodes.Status500InternalServerError,
            Instance = httpContext.Request.Path
        };

        switch (exception)
        {
            case ValidationException:
            case BadRequestException:
                problemDetails.Title = exception.GetType().Name;
                problemDetails.Detail = exception.Message;
                problemDetails.Status = StatusCodes.Status400BadRequest;
                break;
            case NotFoundException:
                problemDetails.Title = exception.GetType().Name;
                problemDetails.Detail = exception.Message;
                problemDetails.Status = StatusCodes.Status404NotFound;
                break;
            default:
                break;
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}