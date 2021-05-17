using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SchAppAPI.DOA
{
    public class UploadMediaContent
    {

        [Required(ErrorMessage = "Media title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Media type is required")]
        public string contentType { get; set; }

        //[Required(ErrorMessage = "Attach content file required")]
        public IFormFile ContentFile { get; set; }

    }
}