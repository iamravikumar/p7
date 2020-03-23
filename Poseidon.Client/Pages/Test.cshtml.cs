using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poseidon.Client.Pages
{
    public class Test : PageModel
    {
        public async Task OnSubmit()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5001/api/bidlist");
        }
    }
}