using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Models.Lesson
{
    public class QuizReport
    {
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public Guid TeacherId { get; set; }
        public User User { get; set; }

        public string MarkObtained { get; set; }
        public string MarkObtainable { get; set; }
        public string PercentageCompletion { get; set; }
        public string TimeTaken { get; set; }
        public IList<QuizUserAnswer> QuizUserAnswers { get; set; }
    }
}
