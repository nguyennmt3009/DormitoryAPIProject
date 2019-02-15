namespace DataAccess.Entities
{
    using System.Collections.Generic;

    public class BrandService : _BaseEntity
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<BillDetail> BillDetails { get; set; }
    }
}
