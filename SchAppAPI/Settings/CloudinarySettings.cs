using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Settings
{
    public class CloudinarySettings
    {
        public string CloudName { get; set; }
        public string APIKey  {get; set;}
        public string APISecret { get; set; } 
        public string APIEnvironmentVariable { get; set; }
        public string PublicId { get; set; }
        public string Overwrite { get; set; }
        public string NotifcationUrl { get; set; }
    }
}
