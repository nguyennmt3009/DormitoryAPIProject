namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class BrandServiceConfig : EntityTypeConfiguration<BrandService>
    {
        public BrandServiceConfig()
        {
            ToTable("BrandServices").HasKey(k => k.Id);

            Property(p => p.Description).IsMaxLength().IsOptional();
            Property(p => p.Price).HasPrecision(18, 10).IsOptional();

            // 1 - n BillDetail
            // Brand 1 - n
            HasRequired(bs => bs.Brand)
                .WithMany(b => b.BrandServices)
                .HasForeignKey(bs => bs.BrandId)
                .WillCascadeOnDelete(true);

            // Service 1 - n
            HasRequired(bs => bs.Service)
                .WithMany(b => b.BrandServices)
                .HasForeignKey(bs => bs.ServiceId)
                .WillCascadeOnDelete(true);

        }
    }
}
