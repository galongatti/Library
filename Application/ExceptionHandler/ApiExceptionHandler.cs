using System.ComponentModel.DataAnnotations;
using Library.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Library.ExceptionHandler;

public class ApiExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ApiExceptionHandler> _logger;

    public ApiExceptionHandler(ILogger<ApiExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        LogLevel level = exception switch
        {
            _ => LogLevel.Error
        };
        
        string requestId = httpContext.Request.Headers["x-request-id"];
      
        _logger.Log(level, exception, "Exception caught while processing request {Method} - {Path} - {RequestId}",
            httpContext?.Request?.Method, httpContext?.Request?.Path, requestId);
    

        (int statusCode, string title, string detail) = GetExceptionDetails(exception);

        
        ProblemDetails details = new()
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Extensions = { ["requestId"] = requestId }
        };

        httpContext.Response.StatusCode = statusCode;

        try
        {
            await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);
        }
        catch (Exception writeEx)
        {
            _logger.LogError(writeEx, "Failed to write error response for exception");
        }

        return true;
    }

    private (int statusCode, string title, string detail) GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            ValidationException validationEx => (
                StatusCodes.Status400BadRequest,
                "Bad Request / Validation Error",
                validationEx.Message
            ),
            AuthorException authorException => (
                StatusCodes.Status400BadRequest,
                "Author Error",
                authorException.Message
            ),
            BookException bookException => (
                StatusCodes.Status400BadRequest,
                "Book Error",
                bookException.Message
            ),
            CategoryException categoryException => (
                StatusCodes.Status400BadRequest,
                "Category Error",
                categoryException.Message
            ),
            UserException userException => (
                StatusCodes.Status400BadRequest,
                "User Error",
                userException.Message
            ),
            LendException lendException => (
                StatusCodes.Status400BadRequest,
                "Lend Error",
                lendException.Message
            ),
            _ => (
                StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "An unexpected error occurred."
            )
        };
    }
}