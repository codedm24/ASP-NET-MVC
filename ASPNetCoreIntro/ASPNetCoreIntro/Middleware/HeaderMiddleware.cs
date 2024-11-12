using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ASPNetCoreIntro.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public HeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                httpContext.Response.Headers.Append("sampleheader", new string[] { "addheadermiddleware" });

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeaderMiddleware>();
        }
    }
}
