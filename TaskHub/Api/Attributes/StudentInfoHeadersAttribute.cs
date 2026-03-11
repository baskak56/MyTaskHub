using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Atributes;

public class StudentInfoHeadersAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.HttpContext.Response.OnStarting(() =>
        {
            context.HttpContext.Response.Headers["X-Student-Group"] = "PU-240930";
            context.HttpContext.Response.Headers["X-Student-Name"] = "Baskov Andrei Alekseevich";
            return Task.CompletedTask;
        });
        await next();
    }
}