using ASPNetCoreIntro.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ASPNetCoreIntro
{
    public class HomeController : ControllerBase
    {
        private readonly ISampleService _sampleService;
        public HomeController(ISampleService service) 
        {
            _sampleService = service;
        }

        [ServiceFilter(typeof(CustomExceptionFilter))]
        public async Task<int> Index(HttpContext context)
        {
            var sb = new StringBuilder();
            sb.Append("<UL>");
            sb.Append(string.Join("", _sampleService.GetSampleStrings().Select(s => $"<li>{s}</li>").ToArray()));
            sb.Append("</UL>");
            await context.Response.WriteAsync(sb.ToString().Div());
            return 200;
        }
    }
}
