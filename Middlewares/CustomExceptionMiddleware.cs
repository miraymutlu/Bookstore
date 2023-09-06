using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerService _loggerService;

    public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
    {
        _next = next;
        _loggerService = loggerService;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = Stopwatch.StartNew();
        try
        {
            string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
            _loggerService.Write(message);
        
            await _next(context);
            watch.Stop();
        
            message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
            _loggerService.Write(message);
        }
        catch (Exception e)
        {
            watch.Stop();
            await HandleException(context, e, watch);
        }
    }

    private Task HandleException(HttpContext context, Exception e, Stopwatch watch)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        string message = "[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message: " + e.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
        _loggerService.Write(message);
        
        var result = JsonConvert.SerializeObject(new { error = e.Message }, Formatting.None);

        return context.Response.WriteAsync(result);

    }
}