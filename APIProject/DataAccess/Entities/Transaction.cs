using System;

namespace DataAccess.Entities
{
    public class Transaction : _BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }
        public int BillId { get; set; }
        public bool IsDeposit { get; set; }
        public int AccountPaymentId { get; set; }
        public AccountPayment AccountPayment { get; set; }
    }
}
