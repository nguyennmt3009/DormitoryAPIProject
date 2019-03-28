namespace DataAccess.Entities
{
    using System.Collections.Generic;

    public class AccountPayment : _BaseEntity
    {
        public decimal Amount { get; set; }
        public int Type { get; set; }
        public int AccountCustomerId { get; set; }
        public AccountCustomer AccountCustomer { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
