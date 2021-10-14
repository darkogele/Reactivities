using Application.Marker;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using FluentValidation.AspNetCore;
using FluentValidation;

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
            services.AddMediatR(typeof(ApplicationMarker).Assembly);

            services.AddAutoMapper(typeof(ApplicationMarker).Assembly);

            services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<ApplicationMarker>());

            //services.AddValidatorsFromAssembly(typeof(ApplicationMarker).Assembly);

            return services;
        }
    }
}