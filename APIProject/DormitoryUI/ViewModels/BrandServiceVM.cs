namespace DormitoryUI.ViewModels
{
    public class BrandServiceCreateVM
    {
        public int ServiceId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class BrandServiceUpdateVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class BrandServiceGetVM
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}