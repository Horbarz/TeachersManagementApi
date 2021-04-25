using SchAppAPI.Contexts;
using SchAppAPI.Models;
using SchAppAPI.Models.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Repository
{
    public class ContentRepository : BaseRepository<Content>, IContentRepository
    {
        public ContentRepository(SchoolDbContext context) : base(context)
        {
        }
    }
}
