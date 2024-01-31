using AuthPermissions.BaseCode.CommonCode;

namespace EasyStockDb.Entity
{
    public class Product : IDataKeyFilterReadWrite
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }

        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public string DataKey { get; set; }

        public ProductUnit ProductUnit { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ICollection<Stock> Stocks { get; set; }
        public ICollection<ProductTransfer> ProductTransfers { get; set; }
        public ICollection<TransactionLine> TransactionLines { get; set; }
    }
}
