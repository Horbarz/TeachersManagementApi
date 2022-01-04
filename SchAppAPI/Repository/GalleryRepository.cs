using SchAppAPI.Contexts;
using SchAppAPI.Models;
using SchAppAPI.Models.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Repository
{
    public class GalleryRepository : BaseRepository<Gallery>, IGalleryRepository
    {
        public GalleryRepository(SchoolDbContext context) : base(context)
        {
        }
    }
}
