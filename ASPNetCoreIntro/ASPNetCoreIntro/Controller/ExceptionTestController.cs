using ASPNetCoreIntro.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ASPNetCoreIntro
{
    public class ExceptionTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ServiceFilter(typeof(CustomExceptionFilter))]
        public async Task<int> ShowError()
        {
            throw new NullReferenceException();
            await Task.Delay(100);
            return 200;
        }
    }
}
