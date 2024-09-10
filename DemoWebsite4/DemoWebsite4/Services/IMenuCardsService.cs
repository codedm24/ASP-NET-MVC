using DemoWebsite4.Models;

namespace DemoWebsite4.Services
{
    public interface IMenuCardsService
    {
        Task AddMenuAsync(Menu1 menu);
        Task DeleteMenuAsync(int id);
        Task<Menu1> GetMenuByIdAsync(int id);
        Task<IEnumerable<Menu1>> GetMenusAsync();
        Task<IEnumerable<MenuCard>> GetMenuCardsAsync();
        Task UpdateMenuAsync(Menu1 menu);
    }
}
