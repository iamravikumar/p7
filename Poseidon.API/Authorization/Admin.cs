using Microsoft.AspNetCore.Authorization;

namespace Poseidon.API.Authorization
{
    public class Admin : IAuthorizationRequirement
    {
        private string Role { get; }

        public Admin(string role)
        {
            Role = role;
        }
    }
}
