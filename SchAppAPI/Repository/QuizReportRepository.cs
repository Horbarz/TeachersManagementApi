using SchAppAPI.Contexts;
using SchAppAPI.Models.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Repository
{
    public class QuizReportRepository: BaseRepository<QuizReport>, IQuizReportRepository
    {
        public QuizReportRepository(SchoolDbContext context) : base(context)
        {

        }
    }
}
