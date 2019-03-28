namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class BrandConfig : EntityTypeConfiguration<Brand>
    {
        public BrandConfig()
        {
            ToTable("Brands").HasKey(k => k.Id);

            Property(p => p.Name).HasMaxLength(255).IsOptional();
            
            // 1 - n Employee
            // 1 - n BrandService
            // 1 - n Apartment
            // 1 - n Customer
        }
    }
}
