using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Console = System.Console;


namespace API.Configuration.Filters.Logs
{
    public class LogFilterAttribute: Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var msLogger = context.HttpContext.RequestServices.GetService<MsSqlLogger>();
            var data = context.ActionArguments.Values;
            var logStr = System.Text.Json.JsonSerializer.Serialize(data);

            msLogger.LoggerManager.Information(logStr);

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           Console.WriteLine("İşlem tamamlandı");
        }
    }
}
