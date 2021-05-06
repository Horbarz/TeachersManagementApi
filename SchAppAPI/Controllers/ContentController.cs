using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchAppAPI.DOA.Requests;
using SchAppAPI.Models;
using SchAppAPI.Models.Lesson;
using SchAppAPI.Repository;
using SchAppAPI.Services;

namespace SchAppAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        public readonly IContentRepository contentRepository;
        public readonly IMediaService mediaService;

        public ContentController(IContentRepository contentRepository, IMediaService mediaService)
        {
            this.contentRepository = contentRepository;
            this.mediaService = mediaService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllContent()
        {
            var contents = await this.contentRepository.GetAll();
            return Ok(contents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContent(Guid id)
        {
            if (!ModelState.IsValid) BadRequest();

            var content = await this.contentRepository.GetById(id);
            return Ok(content);
        }


        [Authorize(Roles = ("Editor, Super-Admin, Admin"))]
        [HttpPut]
        public async Task<IActionResult> UpdateContent(UpdateContentRequest contentRequest)
        {
            if (!ModelState.IsValid) BadRequest();

            var contentToUpdate = new Content
            {
                Id = contentRequest.Id,
                Title = contentRequest.Title,
                contentType = contentRequest.contentType,
                Body = contentRequest.Body
            };
            this.contentRepository.Update(contentToUpdate);
            await this.contentRepository.SaveChangesAsync();
            return Ok(new { status = "success", message = "Content successfully updated" });
        }

        [Authorize(Roles = ("Editor, Super-Admin, Admin"))]
        [HttpDelete]
        public async Task<IActionResult> DeleteContent(Guid id)
        {
            if (!ModelState.IsValid) BadRequest();

            await this.contentRepository.Delete(id);
            await this.contentRepository.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = ("Editor, Super-Admin, Admin"))]
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadContent([FromForm] UploadContentRequest contentRequest)
        {

            if (!ModelState.IsValid) BadRequest();

            var isValidContentType = Enum.TryParse(contentRequest.contentType, out ContentType uploadedContentType);
            if (!isValidContentType) return BadRequest("Invalid content Type");

            var contentUrl = string.Empty;
            switch (uploadedContentType)
            {
                case ContentType.Text:
                    contentUrl = contentRequest.ContentBody;
                    if (contentUrl == null)
                    {
                        return BadRequest("Please input content");
                    }

                    break;

                case ContentType.Image:
                    if (contentRequest.ContentFile == null)
                    {
                        return BadRequest("Upload an Image to proceed");
                    }
                    contentUrl = await mediaService.UploadImageContent(contentRequest.ContentFile.FileName, contentRequest.ContentFile.OpenReadStream());
                    break;

                case ContentType.Video:
                    if (contentRequest.ContentFile == null)
                    {
                        return BadRequest("Upload an Image to proceed");
                    }
                    contentUrl = await mediaService.UploadVideoContent(contentRequest.ContentFile.FileName, contentRequest.ContentFile.OpenReadStream());
                    break;
            }

            var contentToCreate = new Content
            {
                Title = contentRequest.Title,
                contentType = uploadedContentType,
                Body = contentUrl,
                LessonId = contentRequest.LessonId
            };
            await this.contentRepository.Add(contentToCreate);
            await this.contentRepository.SaveChangesAsync();
            return Ok(new { status = "success", message = "Content successfully created" });
        }

        // [HttpPost]
        // public async Task<IActionResult> CreateContent(CreateContentRequest contentRequest)
        // {

        //     if (!ModelState.IsValid) BadRequest();

        //     var contentToCreate = new Content
        //     {
        //         Title = contentRequest.Title,
        //         contentType = contentRequest.contentType,
        //         Body = contentRequest.Body,

        //     };
        //     await this.contentRepository.Add(contentToCreate);
        //     await this.contentRepository.SaveChangesAsync();
        //     return Ok(new { status = "success", message = "Content successfully created" });
        // }

    }
}