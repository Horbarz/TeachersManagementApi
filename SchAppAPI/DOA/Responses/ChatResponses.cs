using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.DOA.Responses
{
    public class ConversationResponse
    {
        public Guid Id { get; set; }
        public bool UserIsSender { get; set; }
        public string Body { get; set; }
        public bool Read { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    public class UnreadChatsResponse
    {
        public Guid Id { get; set; }
        public string Sender { get; set; }
        public string Body { get; set; }
        public bool Read { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    public class ChatListResponse
    {
        public string User { get; set; }
        public string Body { get; set; }
        public bool Read { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
