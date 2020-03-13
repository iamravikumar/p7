using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poseidon.Client.Pages.Authorized
{
    [Authorize]
    public class Index : PageModel
    {
        public void OnGet()
        {
            
        }
    }
}