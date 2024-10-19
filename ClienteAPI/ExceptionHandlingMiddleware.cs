using System.Net;
using System.Net.Http;
using FluentValidation;

namespace ClienteAPI;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (NotFoundException ex)
        {
            await HandleNotFoundExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleGenericExceptionAsync(context, ex);
        }
    }

    private Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var errors = new
        {
            context.Response.StatusCode,
            Message = "Validation failed",
            Errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
        };

        return context.Response.WriteAsJsonAsync(errors);
    }
    private Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;

        var errors = new
        {
            context.Response.StatusCode,
            Message = "Validation failed",
            Errors = ex.Message
        };

        return context.Response.WriteAsJsonAsync(errors);
    }

    private Task HandleGenericExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errors = new
        {
            context.Response.StatusCode,
            Message = "An unexpected error occurred",
            Detail = ex.Message
        };

        return context.Response.WriteAsJsonAsync(errors);
    }
}

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}
