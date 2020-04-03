using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Poseidon.API.ActionFilters;
using Poseidon.API.Data;
using Poseidon.API.Repositories;
using Poseidon.API.Services;
using Poseidon.API.Services.Interfaces;

namespace Poseidon.API.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds authentication to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication("Bearer")
                 .AddJwtBearer(options =>
                 {
                     options.Authority = configuration["IdentityServer"];
                     options.Audience = configuration["API"];
                     options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = false,
                         ValidateIssuer = true,
                         ValidateAudience = false
                     };
                 });
        }

        /// <summary>
        /// Adds controllers to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="environment"></param>
        public static void ConfigureControllers(this IServiceCollection services, IWebHostEnvironment environment)
        {
            if (environment.IsTest())
            {
                services.AddControllers(config =>
                {
                    config.Filters.Add(new LogAttribute());
                    config.Filters.Add(new AllowAnonymousFilter());
                });
            }
            else
            {
                services.AddControllers(config => { config.Filters.Add(new LogAttribute()); });
            }
        }

        /// <summary>
        /// Adds action filter attributes to the application's service collection.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureActionFilterAttributes(this IServiceCollection services)
        {
            services.AddScoped<ValidateModelAttribute>();
        }

        /// <summary>
        /// Adds Swagger to the service collection.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Poseidon API",
                    Version = "v1",
                    Description = "Poseidon API/core engine",
                    Contact = new OpenApiContact
                    {
                        Name = "Jon Karlsen",
                        Email = "karlsen.jonarild@gmail.com",
                        Url = new Uri("https://github.com/jakarlse88")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);

                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(configuration["IdentityServer"] + "/connect/authorize", UriKind.Absolute),
                            Scopes = new Dictionary<string, string>
                            {
                                {"poseidon_api", "Poseidon API"},
                            },
                            TokenUrl = new Uri(configuration["IdentityServer"] + "connect/token")
                        }
                    }
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                        },
                        new List<string>()
                    }
                });
            });
        }

        /// <summary>
        /// Adds the repository wrapper to the service collection.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        /// <summary>
        /// Configures the database context.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PoseidonContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext"));
            });
        }

        /// <summary>
        /// Adds local services to the application's service collection.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureLocalServices(this IServiceCollection services)
        {
            services.AddTransient<IBidListService, BidListService>();
            services.AddTransient<ICurvePointService, CurvePointService>();
            services.AddTransient<IRatingService, RatingService>();
            services.AddTransient<IRuleNameService, RuleNameService>();
            services.AddTransient<ITradeService, TradeService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}