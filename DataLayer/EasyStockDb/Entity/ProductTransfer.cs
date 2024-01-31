using AuthPermissions.BaseCode.CommonCode;

namespace EasyStockDb.Entity
{
    public class ProductTransfer : IDataKeyFilterReadWrite
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int ProductId { get; set; }
        public int StoreIdFrom { get; set; }
        public int StoreIdTo { get; set; }
        public decimal Qty { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string DataKey { get; set; }

        public Product Product { get; set; }
    }
}
