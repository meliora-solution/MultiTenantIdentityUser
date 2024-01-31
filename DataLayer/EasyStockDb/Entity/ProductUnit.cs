using AuthPermissions.BaseCode.CommonCode;

namespace EasyStockDb.Entity
{
    public class ProductUnit : IDataKeyFilterReadWrite
    {
        public int UnitId { get; set; }
        public string Unit { get; set; }
        public string DataKey { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
