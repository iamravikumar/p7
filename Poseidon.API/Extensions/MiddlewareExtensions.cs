using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Poseidon.API.Data;

namespace Poseidon.API.Extensions
{
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Configures the app to use SwaggerUI.
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerUi(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Poseidon API 1.0");
                c.RoutePrefix = string.Empty;
              
                c.OAuthClientId("swagger_ui");
                c.OAuthAppName("Swagger UI");
                c.OAuthClientSecret("secret");
                c.OAuth2RedirectUrl("https://localhost:5001/oauth2-redirect.html");
            });
        }

        /// <summary>
        /// Migrates the application database contexts.
        /// </summary>
        /// <param name="app"></param>
        public static void RunDbMigrations(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<PoseidonContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        /// <summary>
        /// Configures CORS.
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCors(this IApplicationBuilder app)
        {
            app.UseCors(options =>
                options
                    .WithOrigins("https://localhost:5002")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithHeaders(HeaderNames.AccessControlAllowOrigin, "*")
            );
        }
    }
}