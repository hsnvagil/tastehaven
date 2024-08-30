#region

using System.Net;
using Taste_Haven_API.Middlewares.Exceptions;

#endregion

namespace Taste_Haven_API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            InvalidMenuItemIdException => (int)HttpStatusCode.BadRequest,
            MenuItemNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var response = new
        {
            exception.Message,
            IsSuccess = false
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}