using DemoWebsite4.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DemoWebsite4.Services
{
    public class MenuCardsService : IMenuCardsService
    {
        private MenuCardsContext _menuCardsContext;

        public MenuCardsService(MenuCardsContext menuCardsContext)
        {
            _menuCardsContext = menuCardsContext;
        }

        public async Task AddMenuAsync(Menu1 menu)
        {
            menu.MenuId = 0;
            _menuCardsContext.Menu.Add(menu);
            try
            {
                int result = await _menuCardsContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
            return ;
        }

        public Task DeleteMenuAsync(int id)
        {
            Menu1 menu = _menuCardsContext.Menu.Single(m => m.MenuId == id);
            _menuCardsContext.Menu.Remove(menu);

            return _menuCardsContext.SaveChangesAsync();
        }

        public async Task<Menu1> GetMenuByIdAsync(int id)
        {
            return await _menuCardsContext.Menu.SingleOrDefaultAsync<Menu1>(m => m.MenuId == id);
        }

        public async Task<IEnumerable<MenuCard>> GetMenuCardsAsync()
        {
            await EnsureDatabaseCreated();

            var menuCards = _menuCardsContext.MenuCard;
            return await menuCards.ToArrayAsync();
        }

        public async Task<IEnumerable<Menu1>> GetMenusAsync()
        {
            await EnsureDatabaseCreated();
            bool dbConnected = CheckConnection();
            var menus = _menuCardsContext.Menu.Include(m => m.MenuCard);
            return await menus.ToArrayAsync();
        }

        public async Task UpdateMenuAsync(Menu1 menu)
        {
            _menuCardsContext.Entry(menu).State = EntityState.Modified;
            await _menuCardsContext.SaveChangesAsync();
        }

        private bool CheckConnection()
        {
            try
            {
                _menuCardsContext.Database.OpenConnection();
                bool canConnect = _menuCardsContext.Database.CanConnect();
                _menuCardsContext.Database.CloseConnection();
            }
            catch (SqlException sqex)
            { 
                System.Diagnostics.Debug.WriteLine(sqex.Message);
                return false;
            }
            return true;
        }

        public async Task EnsureDatabaseCreated()
        {
            var init = new MenuCardDatabaseInitializer(_menuCardsContext);
            await init.CreateAndSeedDatabaseAsync();
        }
    }
}
