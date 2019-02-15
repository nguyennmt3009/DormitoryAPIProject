namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class ApartmentConfig : EntityTypeConfiguration<Apartment>
    {
        public ApartmentConfig()
        {
            ToTable("Apartments").HasKey(k => k.Id);

            Property(p => p.Location).IsMaxLength().IsUnicode().IsOptional();
            Property(p => p.Name).HasMaxLength(255).IsUnicode().IsOptional();
            Property(p => p.BrandId).IsRequired();


            // 1 - n Post
            // 1 - n RoomType
            // Brand 1 - n 

            HasRequired(a => a.Brand)
                .WithMany(b => b.Apartments)
                .HasForeignKey(a => a.BrandId)
                .WillCascadeOnDelete(false);
        }
    }
}
