using ASPNetCoreIntro.Middleware;
using ASPNetCoreIntro.Services;
using System.Diagnostics;

namespace ASPNetCoreIntro
{
    public class Startup
    {

        public Startup(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        ////public void ConfigureServices(IServiceCollection services)
        ////{
        ////    services.AddTransient<ISampleService, DefaultsSampleService>();
        ////    services.AddTransient<HomeController>();
        ////    services.AddDistributedMemoryCache();
        ////    services.AddSession(options => options.IdleTimeout = TimeSpan.FromSeconds(10));
        ////    //services.AddControllersWithViews(options =>
        ////    //{
        ////    //    options.Filters.Add<CustomExceptionFilter>();
        ////    //});
        ////    //services.AddScoped<CustomExceptionFilter>();
        ////    //services.AddControllersWithViews();
        ////    //services.AddControllers(options =>
        ////    //{
        ////    //    options.Filters.Add<CustomExceptionFilter>();
        ////    //});

        ////    services.AddScoped<CustomExceptionFilter>();
        ////    services.AddControllers();
        ////}

        ////public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        ////{
        ////    app.UseStaticFiles();
        ////    //app.Run(async (context) => {
        ////    //    //await context.Response.WriteAsync("Hello World");
        ////    //    await context.Response.WriteAsync(RequestAndResponseSample.GetRequestInformation(context.Request));
        ////    //});

        ////    //app.Run(async (context) =>
        ////    //{
        ////    //    string result = string.Empty;
        ////    //    switch (context.Request.Path.Value?.ToLower())
        ////    //    {
        ////    //        case "/header":
        ////    //            result = RequestAndResponseSample.GetHeaderInformation(context.Request);
        ////    //            break;
        ////    //        case "/add":
        ////    //            result = RequestAndResponseSample.QueryString(context.Request);
        ////    //            break;
        ////    //        case "/content":
        ////    //            result = RequestAndResponseSample.Content(context.Request);
        ////    //            break;
        ////    //        case "/encoded":
        ////    //            result = RequestAndResponseSample.ContentEncoded(context.Request);
        ////    //            break;
        ////    //        case "/form":
        ////    //            result = RequestAndResponseSample.GetForm(context.Request);
        ////    //            break;
        ////    //        case "/writecookie":
        ////    //            result = RequestAndResponseSample.WriteCookie(context.Response);
        ////    //            break;
        ////    //        case "/readcookie":
        ////    //            result = RequestAndResponseSample.ReadCookie(context.Request);
        ////    //            break;
        ////    //        case "/json":
        ////    //            result = RequestAndResponseSample.GetJson(context.Response);
        ////    //            break;
        ////    //        default:
        ////    //            result = RequestAndResponseSample.GetRequestInformation(context.Request);
        ////    //            break;
        ////    //    }
        ////    //    await context.Response.WriteAsync(result);
        ////    //});

        ////    //app.Run(async (context) =>
        ////    //{
        ////    //    if (context.Request.Path.Value?.ToLower() == "/home")
        ////    //    {
        ////    //        HomeController? controller = app.ApplicationServices.GetService<HomeController>();
        ////    //        System.Diagnostics.Debug.Assert(controller != null, "controller null");
        ////    //        int statusCode = await controller.Index(context);
        ////    //        context.Response.StatusCode = statusCode;
        ////    //        return;
        ////    //    }
        ////    //});
        ////    app.UseSession();
        ////    app.UseHeaderMiddleware();
        ////    //app.UseHeading1Middleware();

        ////    app.Map("/home2", homeApp => {
        ////        homeApp.Run(async context => { 
        ////            HomeController? controller = app.ApplicationServices.GetService<HomeController>();
        ////            Debug.Assert(controller != null);
        ////            int statusCode = await controller.Index(context);
        ////            context.Response.StatusCode = statusCode;
        ////        });
        ////    });

        ////    app.Map("/Session", sessionApp => {
        ////        sessionApp.Run(async context => { 
        ////            await SessionSample.SessionAsync(context);
        ////        });
        ////    });

        ////    PathString remaining = PathString.Empty;
        ////    app.MapWhen(context => context.Request.Path.StartsWithSegments("/configuration", out remaining),
        ////        configApp => {
        ////            configApp.Run(async (context) => {
        ////                if (remaining.StartsWithSegments("/appsettings"))
        ////                {
        ////                    await ConfigSample.AppSettings(context, Configuration);
        ////                }
        ////                else if (remaining.StartsWithSegments("/database"))
        ////                {
        ////                    await ConfigSample.ReadDatabaseConnection(context, Configuration);
        ////                }
        ////                else if (remaining.StartsWithSegments("/secret"))
        ////                {
        ////                    await ConfigSample.UserSecret(context, Configuration);
        ////                }
        ////            });
        ////        });
        ////}
        ///

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(mvcoptions => mvcoptions.EnableEndpointRouting = false);
            services.AddTransient<ISampleService, DefaultsSampleService>();
            services.AddTransient<HomeController>();
            services.AddDistributedMemoryCache();
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromSeconds(10));
            services.AddScoped<CustomExceptionFilter>();
            services.AddControllers(options =>
            {
                //options.Filters.Add<CustomExceptionFilter>(); // Add the filter globally
                options.Filters.Add(new CustomExceptionFilter());
            });
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();

            app.UseSession();
            //app.UseHeaderMiddleware();
            // app.UseHeading1Middleware();

            // Add routing
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Make sure this is included
            });

            //app.UseMvc(routes =>
            //            routes.MapRoute(
            //                name: "default",
            //                template: "{controller}/{action}/{id?}",
            //                defaults: new { controller = "Home", action = "Index" }
            //                )
            //            .MapRoute(
            //                name: "language",
            //                template: "{language}/{controller}/{action}/{id?}",
            //                defaults: new { controller = "Home", action = "Index" },
            //                constraints: new { language = @"(en)|(de)" }
            //                )
            //            .MapRoute(
            //                name: "multipleparameters",
            //                template: "{controller}/{action}/{x}/{y}",
            //                defaults: new { controller = "Home", action = "Add" },
            //                constraints: new { x = @"\d+", y = @"\d+" }
            //                ));

            app.UseMvcWithDefaultRoute();

            app.Map("/default", homeApp =>
            {
                homeApp.Run(async context =>
                {
                    HomeController? controller = app.ApplicationServices.GetService<HomeController>();
                    Debug.Assert(controller != null);
                    int statusCode = await controller.Index(context);
                    context.Response.StatusCode = statusCode;
                });
            });

            app.Map("/home2", homeApp =>
            {
                homeApp.Run(async context =>
                {
                    HomeController? controller = app.ApplicationServices.GetService<HomeController>();
                    Debug.Assert(controller != null);
                    int statusCode = await controller.Index(context);
                    context.Response.StatusCode = statusCode;
                });
            });

            //app.Map("/Session", sessionApp =>
            //{
            //    sessionApp.Run(async context =>
            //    {
            //        await SessionSample.SessionAsync(context);
            //    });
            //});

            //PathString remaining = PathString.Empty;
            //app.MapWhen(context => context.Request.Path.StartsWithSegments("/configuration", out remaining),
            //    configApp =>
            //    {
            //        configApp.Run(async (context) =>
            //        {
            //            if (remaining.StartsWithSegments("/appsettings"))
            //            {
            //                await ConfigSample.AppSettings(context, Configuration);
            //            }
            //            else if (remaining.StartsWithSegments("/database"))
            //            {
            //                await ConfigSample.ReadDatabaseConnection(context, Configuration);
            //            }
            //            else if (remaining.StartsWithSegments("/secret"))
            //            {
            //                await ConfigSample.UserSecret(context, Configuration);
            //            }
            //        });
            //    });



        }

    }
}
