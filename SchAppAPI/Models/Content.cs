﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Models
{
    public class Content : BaseEntity
    {
        public string Title { get; set; }
        public ContentType contentType { get; set; }
        public string Body { get; set; }



    }
}
