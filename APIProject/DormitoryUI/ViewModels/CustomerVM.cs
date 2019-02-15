using System;
using System.ComponentModel.DataAnnotations;

namespace DormitoryUI.ViewModels
{

    public class CustomerCreateVM
    {
        [Required, StringLength(50, ErrorMessage = "Sai ten roi kia ma", MinimumLength = 2)]
        public string Fullname { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, Phone]
        public string Phone { get; set; }
        [Required]
        public DateTimeOffset Birthdate { get; set; }
    }
}