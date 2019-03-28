using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class AccountCustomer : _BaseEntity
    {
        public int CustomerId { get; set; }
        public bool Status { get; set; }
        public ICollection<AccountPayment> AccountPayments { get; set; }
    }
}
