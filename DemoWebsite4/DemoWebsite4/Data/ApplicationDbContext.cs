using DemoWebsite4.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DemoWebsite4.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<DemoWebsite4.Models.LoginViewModel> LoginViewModel { get; set; } = default!;
        public DbSet<DemoWebsite4.Models.ForgotPasswordViewModel> ForgotPasswordViewModel { get; set; } = default!;
        public DbSet<DemoWebsite4.Models.ResetPasswordViewModel> ResetPasswordViewModel { get; set; } = default!;
        //public DbSet<DemoWebsite4.Models.RegisterViewModel> RegisterViewModel { get; set; } = default!;
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("server='HP-TUTAI\\HPHOMESQLSRVR';Database='MenuCardsThroughCode';Integrated Security=SSPI;TrustServerCertificate=true;Trusted_Connection=True;MultipleActiveResultSets=true;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
