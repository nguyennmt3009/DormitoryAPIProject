namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Bill : _BaseEntity
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

    public partial class Bill
    {
        [NotMapped]
        public Room Room { get; set; }
        [NotMapped]
        public Apartment Apartment { get; set; }
    }
}
