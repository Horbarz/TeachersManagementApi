using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.DOA.Requests
{
    public class CreateSubjectRequest
    {
        [Required(ErrorMessage = "Subject Name is required")]
        public string Name { get; set;}
    }

    public class UpdateSubjectRequest
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Subject Name is required")]
        public string Name { get; set; }
    }

}
