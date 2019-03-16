namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class CustomerContractConfig : EntityTypeConfiguration<CustomerContract>
    {
        public CustomerContractConfig()
        {
            ToTable("CustomerContract").HasKey(k => k.Id);

            Property(p => p.IsOwner).IsRequired();

            // Customer 1 - n
            HasRequired(cc => cc.Customer)
                .WithMany(c => c.CustomerContracts)
                .HasForeignKey(cc => cc.CustomerId)
                .WillCascadeOnDelete(true);

            // Contract 1 - n
            HasRequired(cc => cc.Contract)
                .WithMany(c => c.CustomerContracts)
                .HasForeignKey(cc => cc.ContractId)
                .WillCascadeOnDelete(true);
        }
    }
}
