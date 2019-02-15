namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class ServiceConfig : EntityTypeConfiguration<Service>
    {
        public ServiceConfig()
        {
            ToTable("Services").HasKey(k => k.Id);

            Property(p => p.Name).HasMaxLength(255).IsUnicode().IsOptional();

            // 1 - n BrandService
        }
    }
}
