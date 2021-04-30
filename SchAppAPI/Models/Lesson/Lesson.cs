using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Models.Lesson
{
    public class Lesson : BaseEntity
    {
        public string Name { get; set; }
        public int LessonNumber { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
        public Guid ClassId { get; set; }
        public Class Class { get; set; }
        public string Thumbnail { get; set; }
        public bool isTopLesson { get; set; } = false;
        public virtual ICollection<Quiz> Quiz { get; set; }
        public virtual ICollection<Content> Contents { get; set; }

        //Lesson report => teacher id and lesson id
        //Answers to Quiz report-> quiz id and teacher id -> scores, markObtainable, %completion,timetaken,answers(json->list of objects with question id and answer),
        //

    }
}
