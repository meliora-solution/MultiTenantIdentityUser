
using AuthPermissions.AspNetCore.GetDataKeyCode;
using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.BaseCode.DataLayer.EfCode;
using EasyStockContextDb.Context;
using EasyStockDb.Configuration;
using EasyStockDb.Entity;
using Microsoft.EntityFrameworkCore;

namespace EasyStockDb.Context
{
    public class EasyStockDbContext : DbContext, IDataKeyFilterReadOnly
    {
        public string DataKey { get; }

        public EasyStockDbContext(DbContextOptions<EasyStockDbContext> options, IGetDataKeyFromUser dataKeyFilter) : base(options)
        {
            // The DataKey is null when: no one is logged in, its a background service, or user hasn't got an assigned tenant
            // In these cases its best to set the data key that doesn't match any possible DataKey 
            DataKey = dataKeyFilter?.DataKey ?? "Bad key";
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductTransfer> ProductTransfers { get; set; } = null!;
        public DbSet<ProductUnit> ProductUnits { get; set; } = null!;
        public DbSet<Stock> stocks { get; set; } = null!;
        public DbSet<Store> Stores { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<TransactionLine> TransactionLines { get; set; } = null!;


        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
       CancellationToken cancellationToken = default(CancellationToken))
        {
            this.MarkWithDataKeyIfNeeded(DataKey);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("EasyStock");

      
            modelBuilder.ApplyConfiguration(new ContactConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfig());
            modelBuilder.ApplyConfiguration(new ProductTransferConfig());
            modelBuilder.ApplyConfiguration(new ProductUnitConfig());
            modelBuilder.ApplyConfiguration(new StockConfig());
            modelBuilder.ApplyConfiguration(new StoreConfig());
            modelBuilder.ApplyConfiguration(new TransactionConfig());
            modelBuilder.ApplyConfiguration(new TransactionLineConfig());


            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IDataKeyFilterReadWrite).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSingleTenantReadWriteQueryFilter(this);
                }
                else
                {
                    throw new Exception(
                        $"You haven't added the {nameof(IDataKeyFilterReadWrite)} to the entity {entityType.ClrType.Name}");
                }

                foreach (var mutableProperty in entityType.GetProperties())
                {
                    if (mutableProperty.ClrType == typeof(decimal))
                    {
                        mutableProperty.SetPrecision(9);
                        mutableProperty.SetScale(2);
                    }
                }
            }
        }
    }
}
