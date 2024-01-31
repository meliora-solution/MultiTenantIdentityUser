using AuthPermissions.BaseCode.CommonCode;

namespace EasyStockDb.Entity
{
    public class Store : IDataKeyFilterReadWrite
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string DataKey { get; set; }
        public ICollection<Stock> Stocks { get; set; }
        public ICollection<TransactionLine> LineTransactions { get; set; }

    }
}
