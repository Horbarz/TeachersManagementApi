using System;
using System.ComponentModel.DataAnnotations;

namespace SchAppAPI.Models
{
    public class Register
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string SchoolName { get; set; }

        public string Cateory { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Experience { get; set; }

        public SchoolType SchoolType { get; set; }

        public string DeviceToken { get; set; }

    }
}
