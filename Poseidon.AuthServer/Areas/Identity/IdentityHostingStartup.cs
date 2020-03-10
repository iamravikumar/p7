using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poseidon.AuthServer.Areas.Identity.Data;
using Poseidon.AuthServer.Data;

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