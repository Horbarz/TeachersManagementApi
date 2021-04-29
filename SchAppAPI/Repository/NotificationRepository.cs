using SchAppAPI.Contexts;
using SchAppAPI.Models;
using SchAppAPI.Repository;


namespace SchAppAPI.Repository
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(SchoolDbContext context) : base(context)
        {

        }
    }
}
