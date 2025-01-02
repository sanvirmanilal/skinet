using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware;

public class ExceptionMiddleware(IHostEnvironment environment, RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, environment);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex, IHostEnvironment environment)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = environment.IsDevelopment()
            ? new ApiErrorResponse { Message = ex.Message, StatusCode = httpContext.Response.StatusCode, Details = ex.StackTrace }
            : new ApiErrorResponse { Message = ex.Message, StatusCode = httpContext.Response.StatusCode, Details = "Internal Server Error" };

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        return httpContext.Response.WriteAsJsonAsync(response, options);
    }
}
