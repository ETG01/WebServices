using System;
using Microsoft.AspNetCore.Mvc.Filters;
namespace AudioPool.Presentation.Attributes
{
    public class AnalyticsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine(context.HttpContext.Connection.LocalIpAddress);
            Console.WriteLine(context.HttpContext.Request.Path);
        }
    }
}