using Microsoft.AspNetCore.Authorization;

namespace Poseidon.API
{
    public class Admin : IAuthorizationRequirement
    {
        public string Role { get; }

        public Admin(string role)
        {
            Role = role;
        }
    }
}
