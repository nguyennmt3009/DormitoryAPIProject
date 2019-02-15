﻿namespace DataAccess.EntityConfiguration
{
    using DataAccess.Entities;
    using System.Data.Entity.ModelConfiguration;

    public class CustomerConfig : EntityTypeConfiguration<Customer>
    {
        public CustomerConfig()
        {
            ToTable("Customers").HasKey(k => k.Id);

            Property(p => p.Fullname).IsUnicode().HasMaxLength(255).IsOptional();
            Property(p => p.Phone).IsUnicode().HasMaxLength(255).IsOptional();
            Property(p => p.Email).IsUnicode().HasMaxLength(500).IsOptional();
            Property(p => p.Birthdate).IsOptional();

            // 1 - n CustomerContract
        }
    }
}
