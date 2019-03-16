namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class ContractConfig : EntityTypeConfiguration<Contract>
    {
        public ContractConfig()
        {
            ToTable("Contracts").HasKey(k => k.Id);

            Property(p => p.Detail).IsMaxLength().IsOptional();
            Property(p => p.CreatedDate).IsOptional();
            Property(p => p.DueDate).IsOptional();
            Property(p => p.FromDate).IsOptional();
            Property(p => p.Status).IsOptional();
            Property(p => p.Deposit).HasPrecision(18, 10).IsOptional();
            Property(p => p.DueAmount).HasPrecision(18, 10).IsOptional();

            // 1 - n CustomerContract
            // 1 - n Bill

            // Room 1 - n
            HasRequired(c => c.Room)
                .WithMany(r => r.Contracts)
                .HasForeignKey(c => c.RoomId)
                .WillCascadeOnDelete(false);
            
        }
    }
}
