using System;
using System.ComponentModel.DataAnnotations;

namespace SchAppAPI.DOA.Requests
{
    public class CreateQuestionRequest
    {

        [Required(ErrorMessage = "QuizQuestion is required")]
        public string QuizQuestion { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string Answer { get; set; }

        [Required(ErrorMessage = "QuizId is required")]
        public Guid QuizId { get; set; }

    }

    public class UpdateQuestionRequest
    {

        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "QuizQuestion is required")]
        public string QuizQuestion { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string Answer { get; set; }

        [Required(ErrorMessage = "QuizId is required")]
        public Guid QuizId { get; set; }

    }

}