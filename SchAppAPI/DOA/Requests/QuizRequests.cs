using System;
using System.ComponentModel.DataAnnotations;
using SchAppAPI.Models;

namespace SchAppAPI.DOA.Requests
{
    public class CreateQuizRequests
    {
        [Required(ErrorMessage = "Quiz name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Quiz type is required")]
        public QuizType QuizType { get; set; }
    }

    public class UpdateQuizRequests
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Quiz name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Quiz type is required")]
        public QuizType QuizType { get; set; }
    }
}