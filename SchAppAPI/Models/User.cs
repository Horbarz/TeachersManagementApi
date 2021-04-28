using System;
using Microsoft.AspNetCore.Identity;

namespace SchAppAPI.Models
{
    public class User : IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string SchoolName { get; set; }

        public string Cateory { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Experience { get; set; }

        public SchoolType SchoolType { get; set; }

        public bool active { get; set; } = false;

        public string DeviceId { get; set; }
    }
}
