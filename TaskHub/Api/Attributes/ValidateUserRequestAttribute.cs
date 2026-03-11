using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Api.Atributes;

public class ValidateUserRequestAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        object request = null;
        foreach (var arg in context.ActionArguments.Values)
        {
            if (arg != null && arg.GetType().GetProperty("Name") != null)
            {
                request = arg;
                break;
            }
        }
        if (request == null)
        {
            context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
            return;
        }
        var nameProperty = request.GetType().GetProperty("Name");
        var nameValue = nameProperty?.GetValue(request) as string;
        if (string.IsNullOrWhiteSpace(nameValue))
        {
            context.Result = new BadRequestObjectResult("Имя пользователя не задано");
            return;
        }
        await next();
    }
}