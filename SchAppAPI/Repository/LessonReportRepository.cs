using SchAppAPI.Contexts;
using SchAppAPI.Models.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Repository
{
    public class LessonReportRepository: BaseRepository<LessonReport>, ILessonReportRepository
    {
        public LessonReportRepository(SchoolDbContext context): base(context)
        {

        }
    }
}
