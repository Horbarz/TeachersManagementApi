using SchAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.DOA.Responses
{
    public class ClassResponses
    {
        public class GetAllClassResponse
        {
            public List<Class> Classes { get; set; }
        }

    }
}
