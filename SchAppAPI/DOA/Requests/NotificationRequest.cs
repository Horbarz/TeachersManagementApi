namespace SchAppAPI.DOA
{
    public class CreateNotificationRequest
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }

    public class CreateSingleNotificationRequest
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public string DeviceId { get; set; }
    }
}