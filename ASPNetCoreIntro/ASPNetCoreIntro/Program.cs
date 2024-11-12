using ASPNetCoreIntro;
using ASPNetCoreIntro.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNetCoreIntro
{
    public class Program
    {

        public static void Main(string[] args)
        {
            //WebHost.Start(async context => {
            //    await context.Response.WriteAsync("<h1>A simple host!</h1>");
            //}).WaitForShutdown();

            var builder = new WebHostBuilder().UseIIS().UseStartup<Startup>().Build();

            builder.Run();

            //var builder = WebApplication.CreateBuilder(args);
            //var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            //app.Run();

        }
    }
}
