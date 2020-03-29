// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Poseidon.Client.Extensions;
using Poseidon.Client.Areas.Identity.Data;
using Serilog;

namespace Poseidon.Client
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc();

            services.AddLogging();

            services.ConfigureIISOptions();

            services.ConfigureIISServerOptions();

            services.ConfigureIdentityServer(Configuration);

            services.ConfigureAuthentication();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            InitializeDatabase(app);

            CreateIdentityRoles(app, new[] { "Admin", "User" }).Wait();

            CreateTestUsers(app, new[]
            {
                new UserSeed
                {
                    Email = "admin@poseidon.test",
                    Username = "admin@poseidon.test",
                    Password = "Pass123$",
                    Role = "Admin"
                },
                new UserSeed
                {
                    Email = "user@poseidon.test",
                    Username = "user@poseidon.test",
                    Password = "Pass123$",
                    Role = "User"
                }
            }).Wait();

            app.UseStaticFiles();
            
            // app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }

        /**
         * Internal helper methods.
         * 
         */
        
        private class UserSeed
        {
            public string Email { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }
        
        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }

                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.Ids)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }

                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.Apis)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }

                    context.SaveChanges();
                }
            }
        }

        private static async Task CreateIdentityRoles(IApplicationBuilder app, IEnumerable<string> roles)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                foreach (var role in roles)
                {
                    var roleCheck = await roleManager.RoleExistsAsync(role);

                    if (!roleCheck)
                    {
                        var result = await roleManager.CreateAsync(new IdentityRole(role));

                        if (result.Succeeded)
                        {
                            Log.Debug("Role {@role} successfully created", role);
                        }
                        else
                        {
                            Log.Debug("There was a problem creating role {@role}", role);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        private static async Task CreateTestUsers(IApplicationBuilder app, IEnumerable<UserSeed> users)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var userManager =
                    serviceScope.ServiceProvider.GetRequiredService<UserManager<PoseidonAuthServerUser>>();

                foreach (var userSeed in users)
                {
                    var user = await userManager.FindByEmailAsync(userSeed.Email);

                    if (user == null)
                    {
                        var newUser = new PoseidonAuthServerUser
                        {
                            Email = userSeed.Email,
                            UserName = userSeed.Username,
                            EmailConfirmed = true,
                            PhoneNumberConfirmed = true
                        };

                        var password = new PasswordHasher<PoseidonAuthServerUser>();
                        var hashed = password.HashPassword(newUser, userSeed.Password);
                        newUser.PasswordHash = hashed;

                        var result = await userManager.CreateAsync(newUser, userSeed.Password);

                        await userManager.AddToRoleAsync(newUser, userSeed.Role);

                        if (result.Succeeded)
                        {
                            Log.Debug("User [{@user}] successfully created", userSeed);
                        }
                        else
                        {
                            Log.Debug(
                                "There was a problem user {@user}", userSeed);
                        }
                    }
                }
            }
        }
    }
}