using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace DemoWebsite4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string Hello() => "Hello, ASP.NET MVC 6";

        public string Greeting(string name) => HtmlEncoder.Default.Encode($"Hello {name}");


        public string Greeting2(string id) => HtmlEncoder.Default.Encode($"Hello {id}");

        public string Add(int x, int y) => (x+y).ToString();

    }
}
