namespace DataAccess.Entities
{
    using System.Collections.Generic;

    public class Apartment : _BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<RoomType> RoomTypes { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public int? AgencyId { get; set; }
    }
}
