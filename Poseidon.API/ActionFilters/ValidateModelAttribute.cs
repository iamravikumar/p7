using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Poseidon.API.ActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Before controller action invocations, validate that the ModelState is valid.
        /// If invalid, return the ModelState dictionary without proceeding to the action.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}