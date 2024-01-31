using EasyStockDb.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EasyStockDb.Configuration
{
    public class TransactionConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> modelBuilder)
        {
            modelBuilder.HasKey(b => b.TransactionId);
            modelBuilder.Property<int>("TransactionId");

            // Do not use AutoNumber 
            //modelBuilder.Property<int>("TransactionId")
            //  .ValueGeneratedNever()
            //  .HasColumnType("int");

            modelBuilder.Property(b => b.TransactionDate).IsRequired().HasColumnType("DateTime");
            modelBuilder.Property(b => b.ContactId).IsRequired(false).HasColumnType("int");
            modelBuilder.Property(b => b.Description).IsRequired().HasColumnType("varchar(2000)");
            modelBuilder.Property(b => b.CreatedBy).IsRequired().HasColumnType("varchar(100)");
            modelBuilder.Property(b => b.CreatedAt).IsRequired().HasColumnType("DateTime");

            modelBuilder.Property(b => b.DataKey).IsRequired(false).HasColumnType("varchar(12)");

        }
    }
}
