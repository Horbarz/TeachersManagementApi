using System;
using System.ComponentModel.DataAnnotations;

namespace SchAppAPI.Models
{
    public class Logs : BaseEntity
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Action { get; set; }

    }
}
