namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class EmployeeConfig : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfig()
        {
            ToTable("Employees").HasKey(k => k.Id);

            Property(p => p.Fullname).IsUnicode().HasMaxLength(255).IsOptional();
            Property(p => p.Phone).IsUnicode().HasMaxLength(255).IsOptional();
            Property(p => p.Email).IsUnicode().HasMaxLength(500).IsOptional();
            Property(p => p.Birthdate).IsOptional();

            // Role 1 - n
            HasRequired(e => e.Role)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            // Brand 1 - n
            HasRequired(e => e.Brand)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.BrandId)
                .WillCascadeOnDelete(false);
        }
    }
}
