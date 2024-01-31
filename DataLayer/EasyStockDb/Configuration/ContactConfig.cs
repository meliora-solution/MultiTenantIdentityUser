using EasyStockDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyStockDb.Configuration
{
    public class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> modelBuilder)
        {
            modelBuilder.HasKey(b => b.ContactId);
            modelBuilder.Property<int>("ContactId");

            // Do not create AutoNumber 
            //modelBuilder.Property<int>("ContactId")
            //  .ValueGeneratedNever()
            //  .HasColumnType("int");

            modelBuilder.Property(b => b.type).IsRequired().HasColumnType("char(1)");
            modelBuilder.Property(b => b.Name).IsRequired().HasColumnType("varchar(50)");
            modelBuilder.HasIndex(b => b.Name).IsUnique(false);
      
            modelBuilder.Property(b => b.Address).IsRequired(false).HasColumnType("varchar(200)");
            modelBuilder.Property(b => b.Phone).IsRequired(false).HasColumnType("varchar(15)");
            modelBuilder.Property(b => b.Description).IsRequired(false).HasColumnType("varchar(200)");

            modelBuilder.Property(b => b.DataKey).IsRequired(false).HasColumnType("varchar(12)");
        }
    }
}
