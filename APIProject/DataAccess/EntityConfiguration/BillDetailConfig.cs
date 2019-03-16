namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class BillDetailConfig : EntityTypeConfiguration<BillDetail>
    {
        public BillDetailConfig()
        {
            ToTable("BillDetails").HasKey(k => k.Id);

            Property(p => p.Price).HasPrecision(18, 10).IsOptional();
            Property(p => p.Quantity).IsOptional();
            Property(p => p.CreatedDate).IsOptional();
            Property(p => p.IsBuildingRent).IsOptional();

            //BrandService 1 - n
            HasOptional(bd => bd.BrandService)
                .WithMany(bs => bs.BillDetails)
                .HasForeignKey(bd => bd.BrandServiceId)
                .WillCascadeOnDelete(true);

            //Bill 1 - n
            HasRequired(bd => bd.Bill)
                .WithMany(b => b.BillDetails)
                .HasForeignKey(bd => bd.BillId)
                .WillCascadeOnDelete(true);
        }
    }
}
