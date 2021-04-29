using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchAppAPI.DOA.Requests;
using SchAppAPI.DOA.Responses;
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
        public readonly ILessonReportRepository lessonReportRepository;
        public readonly IMapper mapper;


        public LessonController(
            ILessonRepository lessonRepository,
            IContentRepository contentRepository,
            ILessonReportRepository lessonReportRepository,
            IMapper mapper)
        {
            this.lessonRepository = lessonRepository;
            this.contentRepository = contentRepository;
            this.lessonReportRepository = lessonReportRepository;
            this.mapper = mapper;

        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllLessons()
        {
            var lessons = await this.lessonRepository.Get(includeProperties: $"{nameof(Lesson.Subject)},{nameof(Lesson.Class)}");
            var lessonToReturn = this.mapper.Map<List<GetAllLessonReponse>>(lessons);
            return Ok(lessonToReturn);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLesson(Guid id)
        {
            if (!ModelState.IsValid) BadRequest();

            var lesson = await this.lessonRepository.Get(
                lesson => lesson.Id == id,
                null,
                $"{nameof(Lesson.Subject)},{nameof(Lesson.Class)},{nameof(Lesson.Contents)},{nameof(Lesson.Quiz)}");
            if (lesson == null)
            {
                return NotFound("Lesson not found");
            }
            var lessonToReturn = this.mapper.Map<GetLessonReponse>(lesson.FirstOrDefault());

            return Ok(lessonToReturn);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLesson(UpdateLessonRequest lessonRequest)
        {
            if (!ModelState.IsValid) BadRequest();

            var lessonToUpdate = new Lesson
            {
                Id = lessonRequest.Id,
                Name = lessonRequest.Name,
                LessonNumber = lessonRequest.LessonNumber,
                SubjectId = lessonRequest.SubjectId,
                ClassId = lessonRequest.ClassId,
                Thumbnail = lessonRequest.Thumbnail
            };
            this.lessonRepository.Update(lessonToUpdate);

            // var content = new Content
            // {
            //     LessonId = lessonToUpdate.Id,
            //     contentType = ContentType.Text,
            //     Body = lessonRequest.Content,
            //     Title = lessonRequest.Name
            // };

            // this.contentRepository.Update(content);
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
                Name = lessonRequest.Name,
                LessonNumber = lessonRequest.LessonNumber,
                SubjectId = lessonRequest.SubjectId,
                ClassId = lessonRequest.ClassId,
                Thumbnail = lessonRequest.Thumbnail
            };
            await this.lessonRepository.Add(lessonToCreate);

            var content = new Content
            {
                LessonId = lessonToCreate.Id,
                contentType = ContentType.Text,
                Body = lessonRequest.Content,
                Title = lessonRequest.Name

            };

            await this.contentRepository.Add(content);
            await this.lessonRepository.SaveChangesAsync();

            return Ok(new { status = "success", message = "Lesson successfully created" });
        }

        [HttpPost]
        [Route("Report")]
        public async Task<IActionResult> CreatelessonReport(CreateLessonReportRequest lessonReportRequest)
        {

            if (!ModelState.IsValid) BadRequest();


            if (!Guid.TryParse(User.Claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault(), out var userId))
            {
                BadRequest("Invalid User Id");
            }

            var lessonReportToCreate = new LessonReport
            {
                LessonId = lessonReportRequest.LessonId,
                TeacherId = userId,
                IsCompleted = lessonReportRequest.IsCompleted,
                TimeSpentOnModule = Convert.ToString(lessonReportRequest.TimeSpentOnModule),
                CompletionRate = Convert.ToString(lessonReportRequest.TimeSpentOnModule)
            };

            await this.lessonReportRepository.Add(lessonReportToCreate);
            await this.lessonRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("Report")]
        public async Task<IActionResult> UpdatelessonReport(UpdateLessonReportRequest lessonReportRequest)
        {

            if (!ModelState.IsValid) BadRequest();

            if (!Guid.TryParse(User.Identity.Name, out var userId))
            {
                BadRequest("Invalid User Id");
            }

            var lessonReportToCreate = new LessonReport
            {
                LessonId = lessonReportRequest.LessonId,
                TeacherId = userId,
                IsCompleted = lessonReportRequest.IsCompleted,
                TimeSpentOnModule = Convert.ToString(lessonReportRequest.TimeSpentOnModule),
                CompletionRate = Convert.ToString(lessonReportRequest.TimeSpentOnModule)
            };

            this.lessonReportRepository.Update(lessonReportToCreate);
            await this.lessonRepository.SaveChangesAsync();

            return Ok();
        }
    }
}