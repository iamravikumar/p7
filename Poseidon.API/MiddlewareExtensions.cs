using Microsoft.AspNetCore.Builder;

namespace Poseidon.API
{
    public static class MiddlewareExtensions
    {
        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Poseidon API 1.0");
                c.RoutePrefix = string.Empty;
              
                c.OAuthClientId("swagger_ui");
                c.OAuthAppName("Swagger UI");
                c.OAuthClientSecret("secret");
                c.OAuth2RedirectUrl("http://localhost:5001/oauth2-redirect.html");
            });
        }
    }
}