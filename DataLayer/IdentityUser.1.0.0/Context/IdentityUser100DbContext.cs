using IdentityUser100.Configuration;
using IdentityUser100.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityUser100.Context
{
    public partial class IdentityUser100DbContext : IdentityDbContext
    {
        public IdentityUser100DbContext(DbContextOptions<IdentityUser100DbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.ApplyConfiguration(new ApplicationUserConfig());
            modelBuilder.ApplyConfiguration(new PropertyConfig());
            modelBuilder.ApplyConfiguration(new UserPropertyConfig());

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<UserProperty> UserProperties { get; set; }



    }
}
