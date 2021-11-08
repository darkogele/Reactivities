using Application.Interfaces;
using Application.Marker;
using FluentValidation.AspNetCore;
using Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace API.Extensions
{
    public static partial class Register
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Dotnet build in services
            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            // Database configuration
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("SqlLite"));
            });

            // Dependency injection services
            services.AddMediatR(typeof(ApplicationMarker).Assembly);

            services.AddAutoMapper(typeof(ApplicationMarker).Assembly);

            services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<ApplicationMarker>());

            services.AddScoped<IUserAccessor, UserAccessor>();

            return services;
        }
    }
}