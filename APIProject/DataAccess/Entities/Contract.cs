namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;

    public class Contract : _BaseEntity
    {
        public string Detail { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public decimal Deposit { get; set; } // Tien dat coc
        public decimal DueAmount { get; set; } // Tien moi thang
        public bool Status { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public ICollection<CustomerContract> CustomerContracts { get; set; }
        public ICollection<Bill> Bills { get; set; }
    }
}
