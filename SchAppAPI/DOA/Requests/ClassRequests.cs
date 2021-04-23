
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.DOA.Requests
{
    public class CreateClassRequest
    {
        [Required(ErrorMessage = "Class name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Number of terms is required")]
        public int Terms { get; set; }
    }

    public class UpdateClassRequest
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Class Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Number of terms is required")]
        public int Terms { get; set; }
    }
}
