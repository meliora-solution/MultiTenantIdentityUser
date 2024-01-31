using EasyStockDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyStockDb.Configuration
{
    public class TransactionLineConfig : IEntityTypeConfiguration<TransactionLine>
    {
        public void Configure(EntityTypeBuilder<TransactionLine> modelBuilder)
        {
            modelBuilder.HasKey("ProductId", "TransactionId");
            modelBuilder.Property(b => b.TransactionType).IsRequired().HasColumnType("char(2)");
            modelBuilder.Property(b => b.ProductId).IsRequired().HasColumnType("int");
            modelBuilder.Property(b => b.StoreId).IsRequired().HasColumnType("int");
            modelBuilder.Property(b => b.Qty).IsRequired().HasColumnType("Decimal(18,2)");
            modelBuilder.Property(b => b.Price).IsRequired().HasColumnType("Decimal(18,2)");
            modelBuilder.Property(b => b.DataKey).IsRequired(false).HasColumnType("varchar(12)");
            //one to many relation between product and product Transaction. Satu produk bisa banyak transaksi
            modelBuilder
               .HasOne(b => b.Product)
               .WithMany(b => b.TransactionLines).HasForeignKey(b => b.ProductId);

            //one to many relation between product and product Transaction. Satu produk bisa banyak transaksi
            modelBuilder
               .HasOne(b => b.Store)
               .WithMany(b => b.LineTransactions).HasForeignKey(b => b.StoreId);

        }
    }
}
