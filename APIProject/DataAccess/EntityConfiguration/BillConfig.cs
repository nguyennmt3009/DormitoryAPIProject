namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class BillConfig : EntityTypeConfiguration<Bill>
    {
        public BillConfig()
        {
            ToTable("Bills").HasKey(k => k.Id);

            Property(p => p.Status).IsOptional();
            Property(p => p.ToDate).IsOptional();
            Property(p => p.FromDate).IsOptional();
            Property(p => p.CreatedDate).IsOptional();
            Property(p => p.TotalAmount).HasPrecision(18, 10).IsOptional();

            // 1 - n BillDetails
            //Contract 1 - n
            HasRequired(b => b.Contract)
                .WithMany(c => c.Bills)
                .HasForeignKey(b => b.ContractId)
                .WillCascadeOnDelete(true);
        }
    }
}
