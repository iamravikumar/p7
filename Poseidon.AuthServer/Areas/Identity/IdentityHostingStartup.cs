using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Poseidon.AuthServer.Areas.Identity.IdentityHostingStartup))]

namespace Poseidon.AuthServer.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}