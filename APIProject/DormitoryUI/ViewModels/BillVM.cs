﻿namespace DormitoryUI.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class BillCreateVM
    {
        public int ContractId { get; set; }
        public bool Status { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }
    }
    public class BillUpdateVM
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public bool Status { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }
    }

    public class BillGetVM
    {
        public int Id { get; set; }
        public RoomBillGetVM Room { get; set; }
        public ApartmentGetVM Apartment { get; set; }
        public bool Status { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CreatedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<BillDetailGetVM> BillDetails { get; set; }
    }

    public class BillCustomerGetVM
    {
        public int Id { get; set; }
        public string Month { get; set; }
        public string RoomName { get; set; }
        public string ApartmentName { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }
        public ICollection<BillDetailCustomerGetVM> BillDetails { get; set; }
    }
}