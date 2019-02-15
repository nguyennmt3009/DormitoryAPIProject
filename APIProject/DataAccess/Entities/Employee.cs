namespace DataAccess.Entities
{
    using System;

    public class Employee : _BaseEntity
    {
        public string Fullname { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
