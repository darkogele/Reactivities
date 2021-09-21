using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static partial class Register
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reactivities API", Version = "v1" });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options => 
            {
                options.DocumentTitle = "Reactiviteis API";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });

            return app;
        }
    }
}
