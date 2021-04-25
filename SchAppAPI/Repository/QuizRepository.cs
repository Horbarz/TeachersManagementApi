using SchAppAPI.Contexts;
using SchAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Repository
{
    public class QuizRepository : BaseRepository<Quiz>, IQuizRepository
    {
        public QuizRepository(SchoolDbContext context) : base(context)
        {
        }
    }
}
