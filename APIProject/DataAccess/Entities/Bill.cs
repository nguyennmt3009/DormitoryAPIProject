namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;

    public class Bill : _BaseEntity
    {
        public bool Status { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public ICollection<BillDetail> BillDetails { get; set; }
    }
}
