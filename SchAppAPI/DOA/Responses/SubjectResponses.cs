using SchAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.DOA.Responses
{
    public class SubjectResponses
    {
        public class GetAllSubjectsResponse
        {
            public List<Subject> Subjects { get; set; }
        }
    }
}
