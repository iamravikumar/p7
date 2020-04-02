using System;
using System.Linq;
using System.Security.Claims;

namespace Poseidon.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string SubjectId(this ClaimsPrincipal user)
        {
            return user?
                .Claims?
                .FirstOrDefault(c => 
                    c.Type.Equals(
                        "sub",
                        StringComparison.OrdinalIgnoreCase))?
                .Value;
        }
    }
}