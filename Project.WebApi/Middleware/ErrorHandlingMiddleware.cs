using Newtonsoft.Json;
using System.Net;

namespace Project.WebApi.Middleware;

internal class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public ErrorHandlingMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _requestDelegate(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = JsonConvert.SerializeObject(
            new
            {
                code = code,
                error = ex.Message,
            });
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)code;
        return httpContext.Response.WriteAsync(result);
    }
}