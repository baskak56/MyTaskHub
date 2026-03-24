using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.filter
{
    public class RequestLoggingFilter : ActionFilterAttribute
    {
        private readonly ILogger<RequestLoggingFilter> _logger;
        private Stopwatch _stopwatch;

        public RequestLoggingFilter(ILogger<RequestLoggingFilter> logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
            _logger.LogInformation("Начало выполнения экшена: {Method} {Path}",
                context.HttpContext.Request.Method,
                context.HttpContext.Request.Path);

            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            var statusCode = context.HttpContext.Response.StatusCode;
            _logger.LogInformation("Завершение экшена: {Method} {Path}: StatusCode: {StatusCode}, Время выполнения: {ElapsedMs} мс",context.HttpContext.Request.Method,context.HttpContext.Request.Path, statusCode,_stopwatch.ElapsedMilliseconds);
            base.OnActionExecuted(context);
        }
    }
}
