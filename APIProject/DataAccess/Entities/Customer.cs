namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;

    public class Customer : _BaseEntity
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public ICollection<CustomerContract> CustomerContracts { get; set; }
    }
}
