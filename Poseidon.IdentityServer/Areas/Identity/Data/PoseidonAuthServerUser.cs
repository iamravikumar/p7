using Microsoft.AspNetCore.Identity;

namespace Poseidon.Client.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the PoseidonAuthServerUser class
    public class PoseidonAuthServerUser : IdentityUser
    {
        public string Role { get; set; }
    }
}
