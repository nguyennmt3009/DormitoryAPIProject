namespace DataAccess.Entities
{
    using System;

    public class Employee : _BaseEntity
    {
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }

    }
}
