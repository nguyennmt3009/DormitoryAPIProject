namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class RoomConfig : EntityTypeConfiguration<Room>
    {
        public RoomConfig()
        {
            ToTable("Rooms").HasKey(k => k.Id);

            Property(p => p.Name).IsUnicode().HasMaxLength(255).IsOptional();
            Property(p => p.Status).IsOptional();

            // 1 - n Contract

            // RoomType 1 - n
            HasRequired(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeId)
                .WillCascadeOnDelete(false);

            // Apartment 1 - n
            HasRequired(r => r.Apartment)
                .WithMany(a => a.Rooms)
                .HasForeignKey(r => r.ApartmentId)
                .WillCascadeOnDelete(false);
        }
    }
}
