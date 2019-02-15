namespace DataAccess.Entities
{
    using System.Collections.Generic;

    public class Brand : _BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<BrandService> BrandServices { get; set; }
        public ICollection<Apartment> Apartments { get; set; }
    }
}
