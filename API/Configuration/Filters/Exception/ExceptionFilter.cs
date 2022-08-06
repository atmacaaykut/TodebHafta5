using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Configuration.Filters.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Configuration.Filters.Exception
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //veri tabanına loglanabilir
            //mail atılababilir.

            var msLogger = (context.HttpContext.RequestServices.GetService(typeof(MsSqlLogger))) as MsSqlLogger;

            var jsonResult = new JsonResult(
                new
                {
                    error = context.Exception.Message,
                    innerException = context.Exception.InnerException,
                    statusCode = HttpStatusCode.InternalServerError
                }

                );

            msLogger.LoggerManager.Error(jsonResult.Value.ToString());

            context.Result = jsonResult;

            base.OnException(context);
        }
    }
}
