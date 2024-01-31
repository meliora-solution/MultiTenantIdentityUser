using AuthPermissions.BaseCode.CommonCode;

namespace EasyStockDb.Entity
{
    public class ProductCategory : IDataKeyFilterReadWrite
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string DataKey { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
