namespace DataAccess.Entities
{
    using System.Collections.Generic;

    public class RoomType : _BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
