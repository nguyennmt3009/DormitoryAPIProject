namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class AccountPaymentConfig : EntityTypeConfiguration<AccountPayment>
    {
        public AccountPaymentConfig()
        {
            ToTable("AccountPayments").HasKey(k => k.Id);

            HasRequired(x => x.AccountCustomer)
                .WithMany(z => z.AccountPayments)
                .HasForeignKey(y => y.AccountCustomerId)
                .WillCascadeOnDelete(true);
        }
    }

    public class AccountCustomerConfig : EntityTypeConfiguration<AccountCustomer>
    {
        public AccountCustomerConfig()
        {
            ToTable("AccountCustomers").HasKey(k => k.Id);

        }
    }

    public class TransactionConfig : EntityTypeConfiguration<Transaction>
    {
        public TransactionConfig()
        {
            ToTable("Transactions").HasKey(k => k.Id);
            

            HasRequired(x => x.AccountPayment)
                .WithMany(z => z.Transactions)
                .HasForeignKey(y => y.AccountPaymentId)
                .WillCascadeOnDelete(true);
        }
    }

}
