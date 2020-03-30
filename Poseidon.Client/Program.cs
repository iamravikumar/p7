using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

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
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                options.ProviderOptions.Authority = "https://localhost:5000";
                options.ProviderOptions.ClientId = "poseidon_client";
                options.ProviderOptions.ResponseType = "id_token token";

                options.UserOptions.NameClaim = "name";
                options.UserOptions.RoleClaim = "role";

                options.UserOptions.ScopeClaim = "poseidon_api";

                //options.Map
                
            });

            await builder.Build().RunAsync();
        }
    }
}