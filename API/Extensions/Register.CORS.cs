using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static partial class Register
    {
        public static IServiceCollection RegisterCORS(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
            {
                policy
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                      .WithExposedHeaders("WWW-Authenticate", "Pagination")
                      .WithOrigins("http://localhost:3000");
            }));

            return services;
        }

        public static IApplicationBuilder UseCORS(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");

            return app;
        }
    }
}
