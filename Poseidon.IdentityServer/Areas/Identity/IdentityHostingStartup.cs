using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Poseidon.Client.Areas.Identity.IdentityHostingStartup))]

namespace Poseidon.Client.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}