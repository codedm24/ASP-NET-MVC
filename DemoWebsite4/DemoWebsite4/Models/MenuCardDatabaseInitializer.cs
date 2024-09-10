using Microsoft.EntityFrameworkCore;

namespace DemoWebsite4.Models
{
    public class MenuCardDatabaseInitializer
    {
        private static bool _databaseChecked = false;

        public MenuCardDatabaseInitializer(MenuCardsContext context)
        { 
            _context = context;
        }

        private MenuCardsContext _context;

        public async Task CreateAndSeedDatabaseAsync()
        {
            if (!_databaseChecked)
            {
                _databaseChecked = true;

                await _context.Database.MigrateAsync();

                if (_context.MenuCard.Count() == 0) {
                    _context.MenuCard.Add(new MenuCard { Title="Breakfast", Active=true, Order=1 });
                    _context.MenuCard.Add(new MenuCard { Title = "Vegetarian", Active = true, Order = 2 });
                    _context.MenuCard.Add(new MenuCard { Title = "Steaks", Active = true, Order = 3 });
                }


                if (_context.Menu.Count() == 0) {
                    _context.Menu.Add(new Menu1 { Text= "Consommé Célestine (with shredded pancake)", Price=4.80m, Active=true, MenuCardId=1 , Order=1, Type="1",Day=DateTime.Now});
                    //_context.Menu.Add(new Menu1 { Text = "Baked Potato soup", Price = 5.00m, Active = true, MenuCardId = 1, Order = 1, Type = "1", Day = DateTime.Now });
                    //_context.Menu.Add(new Menu1 { Text = "Cheddar Broccoli Soup", Price = 6.20m, Active = true, MenuCardId = 1, Order = 1, Type = "1", Day = DateTime.Now });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
