﻿namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;

    public class Customer : _BaseEntity
    {
        public string Fullname { get; set; }
        public string AvatarUrl { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public ICollection<CustomerContract> CustomerContracts { get; set; }
        public string Username { get; set; }
        public int BrandId { get; set; }
        public bool Status { get; set; }
    }
}
