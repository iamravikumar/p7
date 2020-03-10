using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace Poseidon.Client.Pages
{
    public class CallApiModel : PageModel
    {
        public string Json { get; set; }
        
        public async Task<IActionResult> OnGet()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var content = await client.GetStringAsync("http://localhost:5001/identity");

                Json = JArray.Parse(content).ToString();

                return Page();
            }
        } 
    }
}