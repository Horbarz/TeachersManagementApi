namespace SchAppAPI.Models
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}