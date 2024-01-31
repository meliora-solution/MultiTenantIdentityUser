using EasyStockDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyStockDb.Configuration
{
    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> modelBuilder)
        {
            modelBuilder.HasKey(b => b.StoreId);
            modelBuilder.Property<int>("StoreId");

            // Do not use AutoNumber 
            //modelBuilder.Property<int>("StoreId")
            //  .ValueGeneratedNever()
            //  .HasColumnType("int");

            modelBuilder.Property(b => b.StoreName).IsRequired().HasColumnType("varchar(50)");
            modelBuilder.Property(b => b.DataKey).IsRequired(false).HasColumnType("varchar(12)");


        }
    }
}
