using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DormitoryUI.ViewModels
{
    public class RoomVM
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public int RoomTypeId { get; set; }
    }

    public class RoomCreateVM
    {
        public string Name { get; set; }
        public int RoomTypeId { get; set; }
        public int ApartmentId { get; set; }
    }

    public class RoomUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public int RoomTypeId { get; set; }
        public int ApartmentId { get; set; }
    }


}