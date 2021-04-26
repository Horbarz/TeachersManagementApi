using System;
using System.Collections.Generic;
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

        [Required(ErrorMessage = "Lesson Id is required")]
        public Guid LessonId { get; set; }

    }

    public class UpdateQuizRequests
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Quiz name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Quiz type is required")]
        public QuizType QuizType { get; set; }

        [Required(ErrorMessage = "Lesson Id is required")]
        public Guid LessonId { get; set; }
    }

    public class GradeQuizRequest
    {
        public Guid QuizId { get; set; }
        public string TimeTaken { get; set; }
        public string PercentageCompletion { get; set; }
        public List<AnswerRequest> Answers { get; set; }
    }
    public class AnswerRequest
    {
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
    }
}