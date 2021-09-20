using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Route("getTopLessons")]
        public async Task<IActionResult> GetAllTopLessons()
        {
            var lessons = await this.lessonRepository.Get(Toplesson => Toplesson.isTopLesson == true, includeProperties: $"{nameof(Lesson.Subject)},{nameof(Lesson.Class)}");
            var lessonToReturn = this.mapper.Map<List<GetAllLessonReponse>>(lessons);
            return Ok(lessonToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLesson(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest ();

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

        [Authorize(Roles = ("Editor, Super-Admin, Admin"))]
        [HttpPut]
        public async Task<IActionResult> UpdateLesson(UpdateLessonRequest lessonRequest)
        {
            if (!ModelState.IsValid) return BadRequest ();

            var lessonToUpdate = new Lesson
            {
                Id = lessonRequest.Id,
                Name = lessonRequest.Name,
                LessonNumber = lessonRequest.LessonNumber,
                SubjectId = lessonRequest.SubjectId,
                ClassId = lessonRequest.ClassId,
                Thumbnail = lessonRequest.Thumbnail,
                isTopLesson = lessonRequest.isTopLesson
                
            };
            this.lessonRepository.Update(lessonToUpdate);
            await this.lessonRepository.SaveChangesAsync();
            return Ok(new { status = "success", message = "lesson successfully updated" });
        }


        [Authorize(Roles = ("Super-Admin, Admin"))]
        [HttpDelete]
        public async Task<IActionResult> DeleteLesson(Guid id)
        {
            if (!ModelState.IsValid) return  BadRequest();

            await this.lessonRepository.Delete(id);
            await this.lessonRepository.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = ("Super-Admin, Admin"))]
        [HttpPost]
        public async Task<IActionResult> Createlesson(CreateLessonRequest lessonRequest)
        {
            
            if (!ModelState.IsValid) return BadRequest();

            var lessonToCreate = new Lesson
            {
                Name = lessonRequest.Name,
                LessonNumber = lessonRequest.LessonNumber,
                SubjectId = lessonRequest.SubjectId,
                ClassId = lessonRequest.ClassId,
                Thumbnail = lessonRequest.Thumbnail,
                isTopLesson = lessonRequest.isTopLesson
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

        [Authorize(Roles = ("Teacher"))]
        [HttpGet]
        [Route("GetRecentLessons")]
        public async Task<IActionResult> GetRecentlyStudiedLesson()
        {
            if (!Guid.TryParse(User.Claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault(), out var userId))
            {
                BadRequest("Invalid User Id");
            }

            var lessonReports = await this.lessonReportRepository.Get(lr => lr.TeacherId == userId,
                lr => lr.OrderBy(lr => lr.CreatedOn),
                includeProperties: $"{nameof(LessonReport.Lesson)}");


            var recentLessons = this.mapper.Map<List<GetRecentLessonResponse>>(lessonReports);
            return Ok(recentLessons);
        }

        [Authorize(Roles = ("Teacher"))]
        [HttpGet]
        [Route("GetDownloadedLessons")]
        public async Task<IActionResult> GetDownloadedLessons()
        {

            if (!Guid.TryParse(User.Claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault(), out var userId))
            {
                BadRequest("Invalid User Id");
            }

            var lessonReports = await this.lessonReportRepository.Get(lr => lr.TeacherId == userId &&
                    lr.IsDownloaded == true, includeProperties: $"{nameof(LessonReport.Lesson)}");

            if (lessonReports == null)
            {
                return NotFound("No downloaded lesson");
            }

            var downloadedLessons = this.mapper.Map<List<GetDownloadedLessonResponse>>(lessonReports);
            return Ok(downloadedLessons);

        }

        [Authorize(Roles = ("Teacher"))]
        [HttpPost]
        [Route("Downloaded")]
        public async Task<IActionResult> Downloaded(DownloadLessonReportRequest isDownloaded)
        {
            if (!ModelState.IsValid) BadRequest();


            if (!Guid.TryParse(User.Claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault(), out var userId))
            {
                BadRequest("Invalid User Id");
            }

            var lessonReports = await this.lessonReportRepository.Get(lr => lr.LessonId == isDownloaded.LessonId &&
                                                lr.TeacherId == userId);

            var lessonReport = lessonReports.FirstOrDefault();

            lessonReport.IsCompleted = lessonReport.IsCompleted;
            lessonReport.TimeSpentOnModule = lessonReport.TimeSpentOnModule;
            lessonReport.CompletionRate = lessonReport.CompletionRate;
            lessonReport.IsDownloaded = isDownloaded.IsDownloaded;

            this.lessonReportRepository.Update(lessonReport);
            await this.lessonRepository.SaveChangesAsync();

            return Ok(new { status = true, message = "Lesson Downloaded Successfully" });

        }


        [Authorize(Roles = ("Teacher"))]
        [HttpPost]
        [Route("Report")]
        public async Task<IActionResult> CreateOrUpdatelessonReport(CreateLessonReportRequest lessonReportRequest)
        {

            if (!ModelState.IsValid) return BadRequest();

            if (!Guid.TryParse(User.Claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault(), out var userId))
            {
                BadRequest("Invalid User Id");
            }

            var lessonReports = await this.lessonReportRepository.Get(lr => lr.LessonId == lessonReportRequest.LessonId &&
                                                lr.TeacherId == userId);

            if (lessonReports.Any())
            {
                var lessonReport = lessonReports.FirstOrDefault();

                lessonReport.IsCompleted = lessonReportRequest.IsCompleted;
                lessonReport.TimeSpentOnModule = Convert.ToString(lessonReportRequest.TimeSpentOnModule);
                lessonReport.CompletionRate = Convert.ToString(lessonReportRequest.CompletionRate);

                this.lessonReportRepository.Update(lessonReport);
                await this.lessonRepository.SaveChangesAsync();

                return Ok(new { status = true, message = "Report updated Successfully" });
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

            return Ok(new { status = true, message = "Report added Successfully" });
        }


        [Authorize(Roles = "Admin, Super-Admin")]
        [HttpGet]
        [Route("Report")]
        public async Task<IActionResult> GetAllLessonReport()
        {
            var lessonReport = await this.lessonReportRepository.GetAll();
            return Ok(lessonReport);
        }

        [Authorize(Roles = ("Teacher, Super-Admin, Admin"))]
        [HttpGet]
        [Route("Report/TeacherReports")]
        public async Task<IActionResult> GetAllLessonReportForaTeacher()
        {

            if (!Guid.TryParse(User.Claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault(), out var userId))
            {
                BadRequest("Invalid User Id");
            }

            var lessonReport = await this.lessonReportRepository.Get(lr => lr.TeacherId == userId);
            // var lessonToReturn = this.mapper.Map<List<GetAllLessonReponse>>(lessons);
            return Ok(lessonReport);
        }
    }
}