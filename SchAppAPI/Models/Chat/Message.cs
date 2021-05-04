using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Models.Chat
{
    public class Message:BaseEntity
    {
        public string SenderId { get; set; }
        public User Sender { get; set; }

        public string ReceipientId { get; set; }
        public User Receipient { get; set; }

        public string Body { get; set; }
        public bool Read { get; set; }
    }
}
