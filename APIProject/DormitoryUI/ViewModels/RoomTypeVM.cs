using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DormitoryUI.ViewModels
{
    public class RoomTypeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ApartmentId { get; set; }
    }

    public class RoomTypeCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ApartmentId { get; set; }
    }

    public class RoomTypeUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ApartmentId { get; set; }
    }
}