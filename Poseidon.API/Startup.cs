using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Poseidon.API.ActionFilters;
using Poseidon.API.Extensions;
using Poseidon.API.Services;
using Poseidon.API.Services.Interfaces;

namespace Poseidon.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, 
            IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDbContext(Configuration);

            // if (Environment.IsTest())
            // {
            //     services.AddControllers(config =>
            //         {
            //             config.Filters.Add(new LogAttribute());
            //             config.Filters.Add(new AllowAnonymousFilter());
            //         })
            //         .AddFluentValidation(fv =>
            //             fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            // }
            // else
            // {
            //     services.AddControllers(config =>
            //         {
            //             config.Filters.Add(new LogAttribute());
            //         })
            //         .AddFluentValidation(fv =>
            //             fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            // }
                
            services.ConfigureControllers(Environment);

            services.ConfigureActionFilterAttributes();

            services.ConfigureAuthentication();

            services.ConfigureSwagger();

            services.ConfigureRepositoryWrapper();

            services.AddTransient<IBidListService, BidListService>();

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUi();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}