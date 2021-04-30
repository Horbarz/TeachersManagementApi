using SchAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.DOA.Responses
{
    public class GetAllLessonReponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int LessonNumber { get; set; }
        public string Subject { get; set; }
        public string Class { get; set; }
        public string Thumbnail { get; set; }
        public bool isTopLesson { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
    public class GetLessonReponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int LessonNumber { get; set; }
        public string Subject { get; set; }
        public string Class { get; set; }
        public string Thumbnail { get; set; }
        public bool isTopLesson { get; set; }
        public virtual ICollection<GetLessonQuizResponse> Quiz { get; set; }
        public virtual ICollection<GetLessonContentesponse> Contents { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

    }

    public class GetLessonQuizResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string QuizType { get; set; }
    }
    public class GetLessonContentesponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string contentType { get; set; }
        public string Body { get; set; }
    }
}
