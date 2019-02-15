namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            ToTable("Roles").HasKey(k => k.Id);

            Property(p => p.Name).HasMaxLength(255).IsOptional().IsUnicode();

            // 1 - n Employee
        }
    }
}
