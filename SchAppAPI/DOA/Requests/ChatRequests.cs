using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.DOA.Requests
{
    public class SendMessageRequest
    {
        public string ReceipientEmail { get; set; }
        public string Body { get; set; }
    }
}
