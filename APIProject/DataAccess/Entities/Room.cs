namespace DataAccess.Entities
{
    using System.Collections.Generic;

    public class Room : _BaseEntity
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}
