﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DormitoryUI.ViewModels
{
    public class EmployeeUpdateVM
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int BrandId { get; set; }
    }

    
}