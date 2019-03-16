namespace DataAccess.Entities
{
    using System;

    public class BillDetail : _BaseEntity
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int? BrandServiceId { get; set; }
        public BrandService BrandService { get; set; }
        public int BillId { get; set; }
        public Bill Bill { get; set; }
        public bool IsBuildingRent { get; set; }
    }
}
