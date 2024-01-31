using EasyStockDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyStockDb.Configuration
{
    public class ProductTransferConfig : IEntityTypeConfiguration<ProductTransfer>
    {
        public void Configure(EntityTypeBuilder<ProductTransfer> modelBuilder)
        {
            modelBuilder.HasKey(b => b.TransactionId);
            modelBuilder.Property<int>("TransactionId");

            // Do not create AutoNumber 
            //modelBuilder.Property<int>("TransactionId")
            //  .ValueGeneratedNever()
            //  .HasColumnType("int");


            modelBuilder.Property(b => b.TransactionDate).IsRequired().HasColumnType("DateTime");
            modelBuilder.Property(b => b.ProductId).IsRequired().HasColumnType("int");
            modelBuilder.Property(b => b.StoreIdFrom).IsRequired().HasColumnType("int");
            modelBuilder.Property(b => b.StoreIdTo).IsRequired().HasColumnType("int");
            modelBuilder.Property(b => b.Qty).IsRequired().HasColumnType("Decimal(18,2)");

            modelBuilder.Property(b => b.Description).IsRequired().HasColumnType("varchar(2000)");
            modelBuilder.Property(b => b.CreatedBy).IsRequired().HasColumnType("varchar(100)");
            modelBuilder.Property(b => b.CreatedAt).IsRequired().HasColumnType("DateTime");

            modelBuilder.Property(b => b.DataKey).IsRequired(false).HasColumnType("varchar(12)");
            //one to many relation between product and product Transfer. Satu produk bisa banyak transaksi
            modelBuilder
               .HasOne(b => b.Product)
               .WithMany(b => b.ProductTransfers).HasForeignKey(b => b.ProductId);



        }
    }
}
