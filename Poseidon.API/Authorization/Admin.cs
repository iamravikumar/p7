using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
