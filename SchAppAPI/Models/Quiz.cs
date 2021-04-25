using System;
using System.Collections;
using System.Collections.Generic;

namespace SchAppAPI.Models
{
    public class Quiz : BaseEntity
    {
        public string Name { get; set; }
        public QuizType QuizType { get; set; }
        public List<Question> Question { get; set; }

    }
}