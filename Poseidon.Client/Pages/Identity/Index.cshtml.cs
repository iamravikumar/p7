using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poseidon.Client.Pages.Identity
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            
        }
    }
}