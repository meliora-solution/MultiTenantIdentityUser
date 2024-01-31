using AuthPermissions.BaseCode.CommonCode;

namespace EasyStockDb.Entity
{
    public class Stock : IDataKeyFilterReadWrite
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public decimal qty { get; set; }
        public string DataKey { get; set; }

        public Product Product { get; set; }
        public Store Store { get; set; }

    }
}
