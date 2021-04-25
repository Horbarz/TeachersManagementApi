using System;
using System.ComponentModel.DataAnnotations;
using SchAppAPI.Models;

namespace SchAppAPI.DOA.Requests
{
    public class CreateContentRequest
    {
        [Required(ErrorMessage = "Content title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content type is required")]
        public ContentType contentType { get; set; }

        [Required(ErrorMessage = "Content body is required")]
        public string Body { get; set; }


    }

    public class UpdateContentRequest
    {

        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Content title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content type is required")]
        public ContentType contentType { get; set; }

        [Required(ErrorMessage = "Content body is required")]
        public string Body { get; set; }
    }
}