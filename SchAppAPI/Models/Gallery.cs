using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Models
{
    public class Gallery : BaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ContentType contentType { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }

    }
}
