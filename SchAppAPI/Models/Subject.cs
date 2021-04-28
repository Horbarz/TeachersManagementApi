using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Models
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
