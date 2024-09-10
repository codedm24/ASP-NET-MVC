using DemoWebsite4.Models;
using DemoWebsite4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoWebsite4.Controllers
{
    public class MenuAdminController : Controller
    {
        private readonly IMenuCardsService _service;

        public MenuAdminController(IMenuCardsService service)
        { 
            _service = service;
        }

        public async Task<IActionResult> Index()
        { 
            return View(await _service.GetMenusAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id = 0)
        {
            if (id == null)
                return BadRequest();

            Menu1 menu1 = await _service.GetMenuByIdAsync(id.Value);

            if (menu1 == null)
                return NotFound();

            return View(menu1);
        }

        public async Task<IActionResult> Create()
        { 
            IEnumerable<Menu1> menus = await _service.GetMenusAsync();
            menus = menus.ToList<Menu1>().OrderBy<Menu1,int>(item => item.MenuId);
            int id = menus.Last().MenuId + 1;

            IEnumerable<MenuCard> cards = await _service.GetMenuCardsAsync();
            ViewBag.MenuCards = new SelectList(cards, "MenuCardId", "Title");
            Menu1 newMenu = new Menu1 { MenuId=id};
            return View(newMenu);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Menu1 menu)
        {
            if (ModelState.IsValid) {
                await _service.AddMenuAsync(menu);
                return RedirectToAction("Index");
            }

            IEnumerable<MenuCard> cards = await _service.GetMenuCardsAsync();
            ViewBag.MenuCards = new SelectList(cards, "MenuCardId", "Title");
            return View(menu);
        }

        public async Task<IActionResult> Edit(int? id) { 
            if(id == null)
                return BadRequest();

            Menu1 menu = await _service.GetMenuByIdAsync(id.Value);

            if(menu == null)
                return NotFound();

            IEnumerable<MenuCard> menuCardList = await _service.GetMenuCardsAsync();
            ViewBag.MenuCards = new SelectList(menuCardList, "MenuCardId", "Title", menu.MenuCardId);

            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Edit([Bind("Id","MenuCardId","Text","Price","Order","Type","Day")]Menu1 menu)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateMenuAsync(menu);
                return RedirectToAction("Index");
            }

            IEnumerable<MenuCard> cards = await _service.GetMenuCardsAsync();
            ViewBag.MenuCards = new SelectList(cards, "MenuCardId", "Title", menu.MenuCardId);

            return View(menu);
        }

        public async Task<IActionResult> Delete(int? id)
        { 
            if(id == null)
                return BadRequest();

            Menu1 menu = await _service.GetMenuByIdAsync(id.Value);

            if(menu == null)
                return NotFound();

            return View(menu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Menu1 menu = await _service.GetMenuByIdAsync(id);
            await _service.DeleteMenuAsync(menu.MenuId);
            return RedirectToAction("Index");
        }
    }
}
