using System;
using System.Linq;
using System.Security.Claims;

namespace Poseidon.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Gets the sub claim from a ClaimsPrincipal
        /// (ie. the identity of a user).
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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