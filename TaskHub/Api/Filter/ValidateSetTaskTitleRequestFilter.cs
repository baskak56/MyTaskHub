using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Api.Controllers.Tasks.Request;



namespace Api.filter
{
    public class ValidateSetTaskTitleRequestFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.ActionArguments["request"] as SetTaskTitleRequest;
            if (request == null)
            {
                context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
                return;
            }
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
