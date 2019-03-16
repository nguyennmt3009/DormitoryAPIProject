using System;

namespace DormitoryUI.ViewModels
{
    public class ContractCreateVM
    {
        public string Detail { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public decimal Deposit { get; set; } // Tien dat coc
        public decimal DueAmount { get; set; } // Tien moi thang
        public bool Status { get; set; }
        public int RoomId { get; set; }
    }

    public class ContractUpdateVM
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public decimal Deposit { get; set; } // Tien dat coc
        public decimal DueAmount { get; set; } // Tien moi thang
        public bool Status { get; set; }
        public int RoomId { get; set; }
    }

    public class ContractVM
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string RoomName { get; set; }
        public string ApartmentName { get; set; }
        public string ApartmentLocation { get; set; }

        public string Detail { get; set; } // Quy dinh tro??
        public string FromDate { get; set; }
        public string DueDate { get; set; }
        public string Deposit { get; set; } // Tien dat coc
        public string DueAmount { get; set; } // Tien moi thang
        public bool Status { get; set; }
    }
}