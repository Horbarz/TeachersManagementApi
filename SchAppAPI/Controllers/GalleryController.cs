using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchAppAPI.DOA;
using SchAppAPI.Models;
using SchAppAPI.Repository;
using SchAppAPI.Services;

namespace SchAppAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        public readonly IGalleryRepository galleryRepository;
        public readonly IMediaService mediaService;

        public GalleryController(IGalleryRepository galleryRepository, IMediaService mediaService)
        {
            this.galleryRepository = galleryRepository;
            this.mediaService = mediaService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllContent()
        {
            var galleries = await this.galleryRepository.GetAll();
            return Ok(galleries);
        }

        [Authorize(Roles = ("Editor, Super-Admin, Admin"))]
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadContent([FromForm] UploadMediaContent uploadMedia)
        {

            if (!ModelState.IsValid) BadRequest();

            var isValidContentType = Enum.TryParse(uploadMedia.contentType, out ContentType uploadedContentType);
            if (!isValidContentType) return BadRequest("Invalid content Type");

            var contentUrl = string.Empty;
            switch (uploadedContentType)
            {

                case ContentType.Image:
                    if (uploadMedia.ContentFile == null)
                    {
                        return BadRequest("Upload an Image to proceed");
                    }
                    contentUrl = await mediaService.UploadImageContent(uploadMedia.ContentFile.FileName, uploadMedia.ContentFile.OpenReadStream());
                    break;

                case ContentType.Video:
                    if (uploadMedia.ContentFile == null)
                    {
                        return BadRequest("Upload a Video to proceed");
                    }
                    contentUrl = await mediaService.UploadVideoContent(uploadMedia.ContentFile.FileName, uploadMedia.ContentFile.OpenReadStream());
                    break;
            }

            var mediaToCreate = new Gallery
            {
                Title = uploadMedia.Title,
                contentType = uploadedContentType,
                Url = contentUrl
            };
            await this.galleryRepository.Add(mediaToCreate);
            await this.galleryRepository.SaveChangesAsync();
            return Ok(new { status = "success", message = "Media successfully created" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedia(Guid id)
        {
            if (!ModelState.IsValid) BadRequest();

            var media = await this.galleryRepository.GetById(id);
            return Ok(media);
        }

    }
}