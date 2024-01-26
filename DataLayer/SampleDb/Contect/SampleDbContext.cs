using Microsoft.EntityFrameworkCore;
using SampleDb.Entity;

namespace SampleDb.Contect
{
    public partial class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
