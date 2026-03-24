using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Api.filter
{
    public class StudentInfoHeadersFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers["X-Student-Group"] = "PU-240930";
            context.HttpContext.Response.Headers["X-Student-Name"] = "Baskov Andrei Alekseevich";
        }
    }
}