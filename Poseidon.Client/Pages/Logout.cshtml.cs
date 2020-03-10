using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poseidon.Client.Pages
{
    public class Logout : PageModel
    {
        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}