using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using Poseidon.API.Extensions;
using Serilog;

namespace Poseidon.API.ActionFilters
{
    public class LogAttribute : IAsyncActionFilter
    {
        /// <summary>
        /// On every action invocation, log the user's Id, the HTTP action
        /// the controller name, and the action name.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                await context.HttpContext.AuthenticateAsync("Bearer");
            }
            
            var userId = context.HttpContext.User.SubjectId();
            
            var actionName = context.ActionDescriptor.DisplayName.Split(' ')[0].Split('.');
            
            var split = actionName[^2] + '.' + actionName[^1];
            
            Log.Information($"User [{userId}] {context.HttpContext.Request.Method} [{split}]");
            
            var result = await next();
        }
    }
}