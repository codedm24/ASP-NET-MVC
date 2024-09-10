using DemoWebsite4.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebsite4.Controllers
{
    public class ViewsDemoController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.MyData = "Hello";
            return View();
        }

        public IActionResult PassingData()
        {
            ViewBag.MyData = "Hello from the controller";
            return View();
        }

        public IActionResult PassingAModel()
        {
            var menus = new List<Menu> { 
                new Menu{ 
                    Id=1,
                    Text = "Menu Item 1",
                    Price = 6.9,
                    Category = "Main"
                },
                new Menu
                {
                    Id= 2,
                    Text = "Menu Item 2",
                    Price = 6.9,
                    Category = "Vegetarian"
                },
                new Menu
                {
                    Id = 3,
                    Text = "Menu Item 3",
                    Price = 6.9,
                    Category = "Main"
                }
            };

            return View(menus);
        }

        public IActionResult LayoutSample()
        {
            return View();
        }
    }
}
