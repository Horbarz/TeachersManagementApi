using SchAppAPI.Contexts;
using SchAppAPI.Models;
using SchAppAPI.Models.Lesson;
using SchAppAPI.Repository;


namespace SchAppAPI.Repository
{
    public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(SchoolDbContext context) : base(context)
        {
        }
    }
}
