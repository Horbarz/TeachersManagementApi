using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchAppAPI.DOA.Requests;
using SchAppAPI.Models;
using SchAppAPI.Models.Lesson;
using SchAppAPI.Repository;

namespace SchAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        public readonly IContentRepository contentRepository;

        public ContentController(IContentRepository contentRepository)
        {
            this.contentRepository = contentRepository;
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

        [HttpPut]
        public async Task<IActionResult> UpdateClass(UpdateContentRequest contentRequest)
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


        [HttpDelete]
        public async Task<IActionResult> DeleteContent(Guid id)
        {
            if (!ModelState.IsValid) BadRequest();

            await this.contentRepository.Delete(id);
            await this.contentRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContent(CreateContentRequest contentRequest)
        {

            if (!ModelState.IsValid) BadRequest();

            var contentToCreate = new Content
            {
                Title = contentRequest.Title,
                contentType = contentRequest.contentType,
                Body = contentRequest.Body,

            };
            await this.contentRepository.Add(contentToCreate);
            await this.contentRepository.SaveChangesAsync();
            return Ok(new { status = "success", message = "Content successfully created" });
        }





    }
}