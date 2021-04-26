using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchAppAPI.Models
{
    public enum SchoolType
    {
        private_school, government_school, tertiary
    }
    public enum Gender
    {
        Male, Female, Other
    }

    public enum QuizType
    {
        Survey, Optional, Questions
    }

    public enum ContentType
    {
        Text, Video, Image
    }
}
