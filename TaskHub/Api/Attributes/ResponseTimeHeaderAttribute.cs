using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Atributes;

public class ResponseTimeHeaderAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var sw = Stopwatch.StartNew();
        context.HttpContext.Response.OnStarting(() =>
        {
            sw.Stop();
            context.HttpContext.Response.Headers["X-Response-Time-Ms"] = sw.ElapsedMilliseconds.ToString();
            return Task.CompletedTask;
        });
        await next();
    }
}