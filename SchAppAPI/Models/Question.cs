using System;

namespace SchAppAPI.Models
{
    public class Question : BaseEntity
    {
        public string QuizQuestion { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string Answer { get; set; }
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }

    }
}