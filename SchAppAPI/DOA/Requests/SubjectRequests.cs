using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.DOA.Requests
{
    public class CreateSubjectRequest
    { 
        public string Name { get; set;}
    }

    public class UpdateSubjectRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}
