using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;
using DemoWebsite4.Models;

namespace DemoWebsite4.Models
{
    public class MenuCardsContext : DbContext
    {
        public DbSet<Menu1> Menu { get; set; }
        public DbSet<MenuCard> MenuCard { get; set; }

        public MenuCardsContext(DbContextOptions<MenuCardsContext> options) :base(options)
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Menu1>().ToTable("[MenuCards].[mc].[Menu1]");
            //modelBuilder.Entity<MenuCard>().ToTable("[MenuCards].[mc].[MenuCard]");

            //modelBuilder.HasDefaultSchema("mc");

            modelBuilder.Entity<Menu1>().ToTable("Menu");
            modelBuilder.Entity<MenuCard>().ToTable("MenuCard");

            modelBuilder.Entity<Menu1>().Property(p => p.Text).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Menu1>().Property(p => p.MenuId).ValueGeneratedOnAdd().IsRequired();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<DemoWebsite4.Models.Menu> Menu_1 { get; set; } = default!;
    }

    public class MenuCardsDbContextFactory : IDesignTimeDbContextFactory<MenuCardsContext>
    {
        public MenuCardsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MenuCardsContext>();
            optionsBuilder.UseSqlServer("server='HP-TUTAI\\HPHOMESQLSRVR';Database='MenuCardsThroughCode';Integrated Security=SSPI;TrustServerCertificate=true;Trusted_Connection=True;MultipleActiveResultSets=true;");

            return new MenuCardsContext(optionsBuilder.Options);
        }
    }
}
