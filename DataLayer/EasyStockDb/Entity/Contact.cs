﻿using AuthPermissions.BaseCode.CommonCode;

namespace EasyStockDb.Entity
{
    public class Contact : IDataKeyFilterReadWrite
    {
        public int ContactId { get; set; }

        public string type { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string DataKey { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }

}
