
using WebAppWebAPI.Models;

namespace WebAppWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddMvc().AddXmlSerializerFormatters();
            IBookChapterRepository repos = new SampleBookChapterRepository();
            repos.Init();
            builder.Services.AddSingleton<IBookChapterRepository>(repos);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //var startup = new Startup(builder.Environment);
            //startup.ConfigureServices(builder.Services)

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
