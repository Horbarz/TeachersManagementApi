using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchAppAPI.DOA.Requests;
using SchAppAPI.Models;
using SchAppAPI.Repository;

namespace SchAppAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        public readonly IQuizRepository quizRepository;

        public QuizController(IQuizRepository quizRepository)
        {
            this.quizRepository = quizRepository;
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
                QuizType = quizRequest.QuizType
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
                QuizType = quizRequests.QuizType
            };
            await this.quizRepository.Add(quizToCreate);
            await this.quizRepository.SaveChangesAsync();
            return Ok(new { status = "success", message = "Class successfully created" });
        }

    }
}