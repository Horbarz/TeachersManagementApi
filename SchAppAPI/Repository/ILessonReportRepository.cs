using SchAppAPI.Models;
using SchAppAPI.Models.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Repository
{
    public interface ILessonReportRepository : IBaseRepository<LessonReport>
    {
        // Task<List<Subject>> GetSubjects();
    }
}
