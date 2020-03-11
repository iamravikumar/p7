using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Poseidon.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(
                from c in 
                    User.Claims 
                select new 
                    { c.Type, c.Value }
                );
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return new JsonResult(
                from c in 
                    User.Claims 
                select new 
                    { c.Type, c.Value }
            );
        }
        
        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult Normal()
        {
            return new JsonResult(
                from c in 
                    User.Claims 
                select new 
                    { c.Type, c.Value }
            );
        }
    }
}