using AuthPermissions.BaseCode.CommonCode;

namespace EasyStockDb.Entity
{
    public class TransactionLine : IDataKeyFilterReadWrite
    {
        public int TransactionId { get; set; }
        public string TransactionType { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public decimal Qty { get; set; }
        public decimal Price { get; set; }

        public string DataKey { get; set; }

        public Product Product { get; set; }
        public Store Store { get; set; }
        public Transaction Transaction { get; set; }

    }
}
