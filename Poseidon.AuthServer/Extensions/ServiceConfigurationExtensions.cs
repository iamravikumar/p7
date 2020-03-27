using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poseidon.AuthServer.Areas.Identity.Data;

namespace Poseidon.AuthServer.Extensions
{
    public static class ServiceConfigurationExtensions
    {
        /// <summary>
        /// Configures IIS out-of-proc settings.
        /// See https://github.com/aspnet/AspNetCore/issues/14882.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureIISOptions(this IServiceCollection services)
        {
            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });
        }

        /// <summary>
        /// Configures IIS in-proc settings.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureIISServerOptions(this IServiceCollection services)
        {
            services.Configure<IISServerOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });
        }

        /// <summary>
        /// Configures IdentityServer4.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureAuthServerContext(services, configuration);

            ConfigureDefaultIdentity(services);

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(
                            configuration.GetConnectionString("PoseidonAuthServerIdentityConfigurationConnection"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(
                            configuration.GetConnectionString("PoseidonAuthServerIdentityOperationalConnection"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddAspNetIdentity<PoseidonAuthServerUser>();

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
        }

        /// <summary>
        /// Configures authentication.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to http://localhost:5000/signin-google
                    options.ClientId = "copy client ID from Google here";
                    options.ClientSecret = "copy client secret from Google here";
                });
        }

        /**
         * Internal helper methods.
         * 
         */
        private static void ConfigureDefaultIdentity(IServiceCollection services)
        {
            services.AddDefaultIdentity<PoseidonAuthServerUser>(
                    options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PoseidonAuthServerContext>()
                .AddDefaultTokenProviders();
        }

        private static void ConfigureAuthServerContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PoseidonAuthServerContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("PoseidonAuthServerContextConnection")));
        }
    }
}