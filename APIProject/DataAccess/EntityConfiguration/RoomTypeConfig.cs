namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class RoomTypeConfig : EntityTypeConfiguration<RoomType>
    {
        public RoomTypeConfig()
        {
            ToTable("RoomTypes").HasKey(k => k.Id);

            Property(p => p.Name).HasMaxLength(255).IsUnicode().IsOptional();
            Property(p => p.Description).IsMaxLength().IsUnicode().IsOptional();

            // 1 - n Room

            // Apartment 1 - n
            HasRequired(rt => rt.Apartment)
                .WithMany(a => a.RoomTypes)
                .HasForeignKey(rt => rt.ApartmentId)
                .WillCascadeOnDelete(true);
        }
    }
}
