 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace AudioPool.Presentation.Attributes
{
    public class ApiTokenAuthorizeAttribute : ActionFilterAttribute
    {
        private const string ApiTokenAdmin = "69"; // the scearate key

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("api-token", out var extractedApiToken))
            {
                context.Result = new ContentResult
                {
                    StatusCode = 403,
                    Content = "API token is required."
                };
                return;
            }

            if (!ApiTokenAdmin.Equals(extractedApiToken.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                context.Result = new ContentResult
                {
                    StatusCode = 403,
                    Content = "Invalid API token."
                };
            }
        }
    }
}
