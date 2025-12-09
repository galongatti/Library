using System.ComponentModel.DataAnnotations;
using Library.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Library.ExceptionHandler;

public class ApiExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        (int statusCode, string title, string detail) = GetExceptionDetails(exception);

        ProblemDetails details = new()
        {
            Status = statusCode,
            Title = title,
            Detail = detail
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);

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
            _ => (
                StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "An unexpected error occurred."
            )
        };
    }
}