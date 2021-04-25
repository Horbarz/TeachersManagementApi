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
    public class LessonController : ControllerBase
    {
        public readonly ILessonRepository lessonRepository;
        public readonly IContentRepository contentRepository;

        public LessonController(ILessonRepository lessonRepository, IContentRepository contentRepository)
        {
            this.lessonRepository = lessonRepository;
            this.contentRepository = contentRepository;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllLessons()
        {
            var lessons = await this.lessonRepository.GetAll();
            return Ok(lessons);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLesson(Guid id)
        {
            if (!ModelState.IsValid) BadRequest();

            var lesson = await this.lessonRepository.GetById(id);
            return Ok(lesson);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLesson(UpdateLessonRequest lessonRequest)
        {
            if (!ModelState.IsValid) BadRequest();

            var lessonToUpdate = new Lesson
            {
                Id = lessonRequest.Id,
                LessonNumber = lessonRequest.LessonNumber,
                SubjectId = lessonRequest.SubjectId,
                ClassId = lessonRequest.ClassId,
                Thumbnail = lessonRequest.Thumbnail
            };
            var content = new Content
            {
                LessonId = lessonToUpdate.Id,
                contentType = ContentType.Text,
                Body = lessonRequest.Content,
                Title = lessonRequest.Name
            };
            this.lessonRepository.Update(lessonToUpdate);
            this.contentRepository.Update(content);
            await this.lessonRepository.SaveChangesAsync();
            return Ok(new { status = "success", message = "lesson successfully updated" });
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteLesson(Guid id)
        {
            if (!ModelState.IsValid) BadRequest();

            await this.lessonRepository.Delete(id);
            await this.lessonRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Createlesson(CreateLessonRequest lessonRequest)
        {

            if (!ModelState.IsValid) BadRequest();

            var lessonToCreate = new Lesson
            {
                LessonNumber = lessonRequest.LessonNumber,
                SubjectId = lessonRequest.SubjectId,
                ClassId = lessonRequest.ClassId,
                Thumbnail = lessonRequest.Thumbnail
            };
            var content = new Content
            {
                LessonId = lessonToCreate.Id,
                contentType = ContentType.Text,
                Body = lessonRequest.Content,
                Title = lessonRequest.Name
            };
            await this.lessonRepository.Add(lessonToCreate);
            await this.contentRepository.Add(content);
            await this.lessonRepository.SaveChangesAsync();

            return Ok(new { status = "success", message = "Lesson successfully created" });
        }

    }
}