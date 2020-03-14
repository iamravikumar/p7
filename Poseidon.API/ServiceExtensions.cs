using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Poseidon.API.Data;
using Poseidon.API.Repositories;

namespace Poseidon.API
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds authentication to the service collection.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.Audience = "poseidon_api";
                });
        }

        /// <summary>
        /// Adds Swagger to the service collection.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo {Title = "Poseidon API", Version = "v1"});
            });
        }

        /// <summary>
        /// Adds the repository wrapper to the service collection.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PoseidonContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext"));
            });
        }
    }
}