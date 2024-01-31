using AuthPermissions.BaseCode.CommonCode;

namespace EasyStockDb.Entity
{
    public class Transaction : IDataKeyFilterReadWrite
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int? ContactId { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public bool IsPosted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string DataKey { get; set; }
        public Contact Contact { get; set; }
        public ICollection<TransactionLine> LineTransactions { get; set; }

    }
}
