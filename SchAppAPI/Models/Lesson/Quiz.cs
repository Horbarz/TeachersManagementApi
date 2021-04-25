using System;
using System.Collections;
using System.Collections.Generic;

namespace SchAppAPI.Models.Lesson
{
    public class Quiz : BaseEntity
    {
        public string Name { get; set; }
        public QuizType QuizType { get; set; }
        public List<Question> Questions { get; set; }


    }
}