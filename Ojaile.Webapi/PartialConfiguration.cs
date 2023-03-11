using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Ojaile.Webapi
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class PartialConfiguration
    {
        private readonly RequestDelegate _next;

        public PartialConfiguration(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class PartialConfigurationExtensions
    {
        public static IApplicationBuilder UserPartialConfiguration(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PartialConfiguration>();
        }
    }
}
