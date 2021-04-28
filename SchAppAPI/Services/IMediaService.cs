using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Services
{
    public interface IMediaService
    {
        Task<string> UploadImageContent(string filename, Stream file);
        Task<string> UploadVideoContent(string filename, Stream file);
    }
}
