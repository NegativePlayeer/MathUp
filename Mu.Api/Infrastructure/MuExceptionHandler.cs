using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mu.Api.Infrastructure.Exceptions;

namespace Mu.Api.Infrastructure;

public class MuExceptionHandler : IExceptionHandler
{
    private readonly ILogger<MuExceptionHandler> logger;
    public MuExceptionHandler(ILogger<MuExceptionHandler> logger)
    {
        this.logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        var statusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            BusinessRuleException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

        if (statusCode is StatusCodes.Status500InternalServerError)
        {
            this.logger.LogError(exception, "Unhandled exception for {Method} {Path}", httpContext.Request.Method, httpContext.Request.Path);
        }

        else
        {
            this.logger.LogWarning("{Message} for {Path}", exception.Message, httpContext.Request.Path);
        }

        httpContext.Response.StatusCode = statusCode;
        var title = statusCode is StatusCodes.Status500InternalServerError ? "An unexpected error occurred" : exception.Message;
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}