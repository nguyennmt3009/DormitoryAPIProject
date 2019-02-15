namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class BillDetailConfig : EntityTypeConfiguration<BillDetail>
    {
        public BillDetailConfig()
        {
            ToTable("BillDetails").HasKey(k => k.Id);

            Property(p => p.Price).HasPrecision(12, 10).IsOptional();
            Property(p => p.Quantity).IsOptional();
            Property(p => p.CreatedDate).IsOptional();

            //BrandService 1 - n
            HasRequired(bd => bd.BrandService)
                .WithMany(bs => bs.BillDetails)
                .HasForeignKey(bd => bd.BrandServiceId)
                .WillCascadeOnDelete(false);

            //Bill 1 - n
            HasRequired(bd => bd.Bill)
                .WithMany(b => b.BillDetails)
                .HasForeignKey(bd => bd.BillId)
                .WillCascadeOnDelete(false);
        }
    }
}
