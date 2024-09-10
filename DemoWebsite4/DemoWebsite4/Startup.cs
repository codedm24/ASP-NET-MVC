using DemoWebsite4.Models;
using DemoWebsite4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using DemoWebsite4.Data;
using Microsoft.AspNetCore.Identity;

namespace DemoWebsite4
{
    public class Startup
    {
        public Startup(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("AppSettings.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSqlServer(Configuration.GetConnectionString("MenuCardsConnection"),null,null);
            //services.TryAddSingleton<IHostEnvironment>(new HostingEnvironment { EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") });

            services.AddSqlServer<MenuCardsContext>(Configuration.GetConnectionString("MenuCardsConnection"),
                null, null);

            //services.AddSqlServer<MenuCardsContext>(Configuration.GetConnectionString("MenuCardsConnection"),
            //    null, null);

            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<MenuCardsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MenuCardsConnection")));

            //        services.AddDbContext<MenuCardsContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("MenuCardsConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.AddDbContext<MenuCardsContext>(options => options.UseSqlServer(Configuration["Data:MenuCardConnection:ConnectionString"]));
            services.AddMvc(mvcoptions => mvcoptions.EnableEndpointRouting = false);

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddScoped<IMenuCardsService, MenuCardsService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            //app.UseIISPlatformHandler();
            app.UseStaticFiles();

            //app.UseAntiforgery();

            

            app.UseMvc(routes =>
            routes.MapRoute(
                name: "default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" }
                )
            .MapRoute(
                name: "language",
                template: "{language}/{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" },
                constraints: new { language = @"(en)|(de)" }
                )
            .MapRoute(
                name: "multipleparameters",
                template: "{controller}/{action}/{x}/{y}",
                defaults: new { controller = "Home", action = "Add" },
                constraints: new { x = @"\d+", y = @"\d+" }
                ));

            app.UseMvcWithDefaultRoute();
        }
    }
}
