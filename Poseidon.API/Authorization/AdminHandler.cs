using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Poseidon.API.Authorization
{
    public class AdminHandler : AuthorizationHandler<Admin>
    {
        /// <summary>
        /// Handle "Admin" role claim authorization.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Admin requirement)
        {
            var role = 
                context.User.FindFirst(c => c.Type == "role").Value;

            if (role.ToLower() == "admin")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
