using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Models.Lesson
{
    public class LessonReport : BaseEntity
    {
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public Guid TeacherId { get; set; }
        public User User { get; set; }
        public string CompletionRate { get; set; }
        public string TimeSpentOnModule { get; set; }
        public bool IsCompleted { get; set; }

    }
}
