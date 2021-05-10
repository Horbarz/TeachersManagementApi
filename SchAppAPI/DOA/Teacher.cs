using System;
using System.ComponentModel.DataAnnotations;
using SchAppAPI.Models;

namespace SchAppAPI.DOA
{
    public class Teacher
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string SchoolName { get; set; }
        public string Category { get; set; }
        public Location location { get; set; }
        public string Experience { get; set; }
        public SchoolType SchoolType { get; set; }

    }

    public class ResponseTeacher
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string SchoolName { get; set; }
        public string Category { get; set; }
        public string location { get; set; }
        public string Experience { get; set; }
        public string SchoolType { get; set; }

    }
}
