using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormitoryUI.ViewModels
{
    public class ApartmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int BrandId { get; set; }
    }

    public class ApartmentCreateVM
    {
        public string Name { get; set; }
        public string Location { get; set; }
        [Required]
        public int BrandId { get; set; }
        
        public int? AgencyId { get; set; }
    }

    public class ApartmentUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int? Agency { get; set; }
    }
}