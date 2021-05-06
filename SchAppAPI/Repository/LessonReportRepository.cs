using Microsoft.EntityFrameworkCore;
using SchAppAPI.Contexts;
using SchAppAPI.Models;
using SchAppAPI.Models.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Repository
{
    public class LessonReportRepository : BaseRepository<LessonReport>, ILessonReportRepository
    {
        public readonly SchoolDbContext dbContext;
        public LessonReportRepository(SchoolDbContext context) : base(context)
        {
            this.dbContext = context;
        }

        // public async Task<List<Subject>> GetSubjects()
        // {
        //     var res = dbContext.LessonReports.Include(x => x.Lesson).ThenInclude(x => x.Subject).ToListAsync();
        // }
    }
}
