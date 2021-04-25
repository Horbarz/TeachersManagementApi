using System;
using System.ComponentModel.DataAnnotations;

namespace SchAppAPI.DOA.Requests
{
    public class CreateLessonRequest
    {
        [Required(ErrorMessage = "Lesson name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lesson number is required")]
        public int LessonNumber { get; set; }

        [Required(ErrorMessage = "Subject id is required")]
        public Guid SubjectId { get; set; }
        public Guid ClassId { get; set; }

        [Required(ErrorMessage = "Content id is required")]
        public Guid ContentId { get; set; }
        public string Thumbnail { get; set; }
        public Guid QuizId { get; set; }

    }

    public class UpdateLessonRequest
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Lesson name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lesson number is required")]
        public int LessonNumber { get; set; }

        [Required(ErrorMessage = "Subject id is required")]
        public Guid SubjectId { get; set; }
        public Guid ClassId { get; set; }

        [Required(ErrorMessage = "Content id is required")]
        public Guid ContentId { get; set; }
        public string Thumbnail { get; set; }
        public Guid QuizId { get; set; }

    }
}