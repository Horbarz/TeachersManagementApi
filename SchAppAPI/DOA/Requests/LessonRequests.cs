using SchAppAPI.Models.Lesson;
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
        public string Thumbnail { get; set; }
        public bool isTopLesson { get; set; }
        public string Content { get; set; }

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

        [Required(ErrorMessage = "Class id is required")]
        public Guid ClassId { get; set; }

        public bool isTopLesson { get; set; }

        public string Content { get; set; }

        public string Thumbnail { get; set; }

    }

    public class CreateLessonReportRequest
    {
        public Guid LessonId { get; set; }
        public string CompletionRate { get; set; }
        public string TimeSpentOnModule { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class UpdateLessonReportRequest
    {
        public Guid LessonId { get; set; }

        public string CompletionRate { get; set; }
        public string TimeSpentOnModule { get; set; }

        public bool IsCompleted { get; set; }
    }

    public class DownloadLessonReportRequest
    {
        public Guid LessonId { get; set; }
        public bool IsDownloaded { get; set; }
    }
}