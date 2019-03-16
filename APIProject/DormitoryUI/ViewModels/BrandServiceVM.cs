namespace DormitoryUI.ViewModels
{
    public class BrandServiceCreateVM
    {
        public int ServiceId { get; set; }
        public int BrandId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class BrandServiceUpdateVM
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int BrandId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}