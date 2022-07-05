using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ojaile.Client.Model;

namespace Ojaile.Client.Data
{
    public class MyDbContext: IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        //public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Country>(c =>
            //{
            //    c.HasKey(p => p.Id);
            //});
        }
    }
}
