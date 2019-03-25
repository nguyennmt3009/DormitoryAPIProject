namespace DormitoryUI.ViewModels
{
    public class BillDetailCreateVM
    {
        public int BrandServiceId { get; set; }
        public int BillId { get; set; }
        public int Quantity { get; set; }
    }

    public class BillDetailUpdateVM
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class BillDetailCustomerGetVM
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class BillDetailGetVM
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string CreatedDate { get; set; }
        public int? BrandServiceId { get; set; }
        public BrandServiceGetVM BrandService { get; set; }
        public bool IsBuildingRent { get; set; }
    }

}