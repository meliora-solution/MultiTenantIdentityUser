using EasyStockDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many

namespace EasyStockDb.Configuration
{
    public class StockConfig : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> modelBuilder)
        {
            modelBuilder.HasKey(b => new { b.ProductId, b.StoreId });
            modelBuilder.Property(b => b.qty).IsRequired(true).HasColumnType("Decimal(18,2)");
            modelBuilder.Property(b => b.DataKey).IsRequired(false).HasColumnType("varchar(12)");
            //one to many relation between product and Stock.
            modelBuilder
               .HasOne(b => b.Product)
               .WithMany(b => b.Stocks).HasForeignKey(b => b.ProductId);

            //one to many relation between Store and Stock.  
            modelBuilder
               .HasOne(b => b.Store)
               .WithMany(b => b.Stocks).HasForeignKey(b => b.StoreId);

        }
    }
}
