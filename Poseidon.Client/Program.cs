using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Poseidon.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBaseAddressHttpClient();

            builder.Services.AddOidcAuthentication(options =>
            {
                options.ProviderOptions.Authority = "https://localhost:5000";
                options.ProviderOptions.ClientId = "poseidon_client";
                options.ProviderOptions.ResponseType = "id_token token";

                options.UserOptions.NameClaim = "name";
                options.UserOptions.RoleClaim = "role";
                options.UserOptions.ScopeClaim = "poseidon_api";
            });

            await builder.Build().RunAsync();
        }
    }
}