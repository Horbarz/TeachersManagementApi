using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using SchAppAPI.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchAppAPI.Services
{
    public class MediaService: IMediaService
    {
        public readonly CloudinarySettings cloudinarySettings;
        public Cloudinary cloudinary;

        public MediaService(IOptions<CloudinarySettings> cloudinarySettings)
        {
            this.cloudinarySettings = cloudinarySettings.Value;
            SetupConnection();
        }

        private void SetupConnection()
        {
            Account account = new Account(cloudinarySettings.CloudName, cloudinarySettings.APIKey, cloudinarySettings.APISecret);
            cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageContent(string filename, Stream file)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(filename, file)
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            var uri = uploadResult.SecureUrl.AbsoluteUri;
            if (string.IsNullOrWhiteSpace(uri)) throw new Exception("Unable to complete request");

            return uri;

        }
        public async Task<string> UploadVideoContent(string filename, Stream file)
        {
            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(filename, file),
                PublicId = $"Lessons/{filename}",
                Overwrite = true
                //NotificationUrl = "http://mysite/my_notification_endpoint"
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            var uri = uploadResult.SecureUrl.AbsoluteUri;
            if (string.IsNullOrWhiteSpace(uri)) throw new Exception("Unable to complete request");

            return uri;

        }
    }
}
