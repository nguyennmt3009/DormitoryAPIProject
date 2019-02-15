namespace DataAccess.Entities
{
    using System.Collections.Generic;

    public class Service : _BaseEntity
    {
        public string Name { get; set; }
        public ICollection<BrandService> BrandServices { get; set; }
    }
}
