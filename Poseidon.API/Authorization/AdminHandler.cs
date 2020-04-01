using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poseidon.API
{
    public class AdminHandler : AuthorizationHandler<Admin>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Admin requirement)
        {
            var role = context.User.FindFirst(c => c.Type == "role").Value;

            if (role.ToLower() == "admin")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
