using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance;

namespace API.Extensions
{
    public static partial class Register
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Dotnet build in services
            services.AddControllers();

            // Database configuration
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("SqlLite"));
            });

            // Dependensi injection services

            return services;
        }
    }
}