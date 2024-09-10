using Microsoft.AspNetCore.Hosting;
namespace DemoWebsite4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args);
            //var app = builder.Build();
            //app.Run();

            //app.MapGet("/", () => "Hello World!");

            //app.Run();

            var host = new WebHostBuilder()
                //.UseDefaultConfiguration(args)
                .UseIIS()
                //.UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();

            //var builder = new WebHostBuilder().UseIIS().UseStartup<Startup>().Build();

            //builder.Run();
        }
    }
}
