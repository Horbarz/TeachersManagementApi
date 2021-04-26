using System.Collections.Generic;

namespace SchAppAPI.DOA
{
    public class MailRequest
    {
        public List<string> ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}