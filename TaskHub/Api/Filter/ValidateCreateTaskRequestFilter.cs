using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Api.Controllers.Tasks.Request;



namespace Api.filter
{
    public class ValidateCreateTaskRequestFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.ActionArguments["request"] as CreateTaskRequest; ;
            if (request == null)
            {
                context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
                return;
            }
            if (request.UserId == Guid.Empty)
            {
                context.Result = new BadRequestObjectResult("Идентификатор пользователя не задан");
                return;
            }
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                context.Result = new BadRequestObjectResult("Название задачи не задано");
                return;
            }
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
