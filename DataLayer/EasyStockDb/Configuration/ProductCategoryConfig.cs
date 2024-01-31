using EasyStockDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyStockDb.Configuration
{
    public class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> modelBuilder)
        {
            modelBuilder.HasKey(b => b.CategoryId);
            modelBuilder.Property<int>("CategoryId");
            // Do not create AutoNumber 
            //modelBuilder.Property<int>("CategoryId")
            //  .ValueGeneratedNever()
            //  .HasColumnType("int");
            modelBuilder.HasIndex(b => b.Category).IsUnique(false);
            modelBuilder.Property(b => b.Category).IsRequired(true).HasColumnType("varchar(50)");
            modelBuilder.Property(b => b.DataKey).IsRequired(false).HasColumnType("varchar(12)");
        }
    }
}
