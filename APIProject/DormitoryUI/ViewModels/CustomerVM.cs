using System;
using System.ComponentModel.DataAnnotations;

namespace DormitoryUI.ViewModels
{
    public class CustomerCreateVM
    {
        public string LastName { get; set; }
        [Required, StringLength(50, ErrorMessage = "Sai ten roi kia ma", MinimumLength = 2)]
        public string FirstName { get; set; }
        public string Email { get; set; }
        [Required, Phone]
        public string Phone { get; set; }
        [Required]
        public DateTimeOffset Birthdate { get; set; }
        public string Sex { get; set; }
        public int? ContractId { get; set; }
    }

    public class CustomerUpdateVM
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public string Sex { get; set; }

    }

    public class CustomerGetVM
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Birthdate { get; set; }
        public string Sex { get; set; }
        public string Username { get; set; }

    }

}