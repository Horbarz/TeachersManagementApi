using System;
using System.Linq;
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
    public class QuizController : ControllerBase
    {
        public readonly IQuizRepository quizRepository;
        public readonly IQuizReportRepository quizReportRepository;

        public QuizController(IQuizRepository quizRepository, IQuizReportRepository quizReportRepository)
        {
            this.quizRepository = quizRepository;
            this.quizReportRepository = quizReportRepository;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllQuiz()
        {
            var quizzes = await this.quizRepository.GetAll();
            return Ok(quizzes);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuiz(Guid id)
        {
            if (!ModelState.IsValid) BadRequest();

            var quiz = await this.quizRepository.GetById(id);
            return Ok(quiz);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuiz(UpdateQuizRequests quizRequest)
        {
            if (!ModelState.IsValid) BadRequest();
            var quizToUpdate = new Quiz
            {
                Id = quizRequest.Id,
                Name = quizRequest.Name,
                QuizType = quizRequest.QuizType,
                LessonId = quizRequest.LessonId
            };
            this.quizRepository.Update(quizToUpdate);
            await this.quizRepository.SaveChangesAsync();
            return Ok(new { status = "success", message = "Quiz successfully updated" });
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        {
            if (!ModelState.IsValid) BadRequest();

            await this.quizRepository.Delete(id);
            await this.quizRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz(CreateQuizRequests quizRequests)
        {

            if (!ModelState.IsValid) BadRequest();

            var quizToCreate = new Quiz
            {
                Name = quizRequests.Name,
                QuizType = quizRequests.QuizType,
                LessonId = quizRequests.LessonId
            };
            await this.quizRepository.Add(quizToCreate);
            await this.quizRepository.SaveChangesAsync();
            return Ok(new { status = "success", message = "Class successfully created" });
        }

        [HttpPost]
        [Route("gradequiz")]
        public async Task<IActionResult> GradeQuiz(GradeQuizRequest gradeQuizRequest)
        {

            if (!ModelState.IsValid) BadRequest();

            if (!Guid.TryParse(User.Claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault(), out var userId))
            {
                BadRequest("Invalid User Id");
            }

            //TODO: Implement Grade quiz

            var quizReport = new QuizReport
            {
                QuizId = gradeQuizRequest.QuizId,
                TeacherId = userId,
                MarkObtainable = "10", //to be filled with response from grad quiz
                MarkObtained = "5", //to be filled with response from grad quiz
                TimeTaken = gradeQuizRequest.TimeTaken,
                PercentageCompletion = gradeQuizRequest.PercentageCompletion,
                QuizUserAnswers = gradeQuizRequest.Answers
                                        .Select(x => new QuizUserAnswer
                                        {
                                            QuestionId = Convert.ToString(x.QuestionId),
                                            Answer = x.Answer
                                        }).ToList()
            };
            await this.quizReportRepository.Add(quizReport);
            await this.quizReportRepository.SaveChangesAsync();
            return Ok();
        }

    }
}