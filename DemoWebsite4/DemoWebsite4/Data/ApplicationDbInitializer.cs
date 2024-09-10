using Microsoft.EntityFrameworkCore;

namespace DemoWebsite4.Data
{
    public class ApplicationDbInitializer
    {
        public static bool _databaseChecked = false;

        public ApplicationDbInitializer(ApplicationDbContext context)
        { 
            _context = context;
        }

        private ApplicationDbContext _context;

        public async Task CreateDatabaseAsync()
        {
            try
            {
                if (!_databaseChecked)
                {
                    _databaseChecked = true;

                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex) {
                int tt = 0;
            }

            await _context.SaveChangesAsync();
        }
    }
}
