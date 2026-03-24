using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

public class FromRouteTaskIdAttribute : Attribute, IModelBinder, IModelBinderProvider
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var idValue = bindingContext.ActionContext.RouteData.Values["id"];
        if (idValue == null)
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Идентификатор задачи не задан");
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }
        string idString = idValue.ToString();
        if (Guid.TryParse(idString, out var parsedGuid))
        {
            bindingContext.Result = ModelBindingResult.Success(parsedGuid);
            return Task.CompletedTask;
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Идентификатор задачи имеет некорректный формат");
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;

        }
    }
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(Guid)) return this;
        else return null;
    }
}
