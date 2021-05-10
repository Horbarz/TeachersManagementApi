using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
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

        [Required(ErrorMessage = "Lesson Id is required")]
        public Guid LessonId { get; set; }


    }

    public class UploadContentRequest
    {
        [Required(ErrorMessage = "Content title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content type is required")]
        public string contentType { get; set; }

        //[Required(ErrorMessage = "Attach content file required")]
        public IFormFile ContentFile { get; set; }

        public string ContentBody { get; set; }

        [Required(ErrorMessage = "Lesson Id is required")]
        public Guid LessonId { get; set; }


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

        [Required(ErrorMessage = "Lesson Id is required")]
        public Guid LessonId { get; set; }

        public IFormFile ContentFile { get; set; }
    }


}