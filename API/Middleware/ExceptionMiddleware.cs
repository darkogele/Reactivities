using System.Net;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Application.Core;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ILogger<ExceptionMiddleware> _logger { get; }
        public IHostEnvironment _environment { get; }

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _environment = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, message: ex.Message);
                await HandleExceptionsAsync(context, ex);
            }
        }

        private async Task HandleExceptionsAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _environment.IsDevelopment()
                ? new AppExceptions(httpContext.Response.StatusCode, exception.Message, exception.StackTrace?.ToString())
                : new AppExceptions(httpContext.Response.StatusCode, "Server Error");

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, jsonOptions);

            await httpContext.Response.WriteAsync(json);
        }
    }
}