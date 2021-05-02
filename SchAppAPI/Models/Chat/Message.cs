using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Models.Chat
{
    public class Message:BaseEntity
    {
        public Guid SenderId { get; set; }
        public User Sender { get; set; }
        public Guid ReceipientId { get; set; }
        public User Receipient { get; set; }
        public string Body { get; set; }
        public bool Read { get; set; }
    }
}
