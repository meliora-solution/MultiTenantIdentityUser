using EasyStockDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyStockDb.Configuration
{
    public class ProductUnitConfig : IEntityTypeConfiguration<ProductUnit>
    {
        public void Configure(EntityTypeBuilder<ProductUnit> modelBuilder)
        {
            modelBuilder.HasKey(b => b.UnitId);
            modelBuilder.Property<int>("UnitId");

            // jangan buat AutoNumber 
            //modelBuilder.Property<int>("UnitId")
            //  .ValueGeneratedNever()
            //  .HasColumnType("int");

            modelBuilder.HasIndex(b => b.Unit).IsUnique(false);
            modelBuilder.Property(b => b.Unit).IsRequired().HasColumnType("varchar(50)");
            modelBuilder.Property(b => b.DataKey).IsRequired(false).HasColumnType("varchar(12)");
        }
    }
}
