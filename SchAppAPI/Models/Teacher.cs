using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchAppAPI.Models
{
    public enum SchoolType
    {
        private_school, government_school, tertiary
    }


    [Table("Teachers")]
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        public Gender Gender { get; set; }

        public string SchoolName { get; set; }

        public string Cateory { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Experience { get; set; }

        public SchoolType SchoolType { get; set; }

    }
}
