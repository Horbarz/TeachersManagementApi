using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchAppAPI.DOA.Requests;
using SchAppAPI.Models;
using SchAppAPI.Models.Lesson;
using SchAppAPI.Repository;

namespace SchAppAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        public readonly IQuestionRepository questionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await this.questionRepository.GetAll();
            return Ok(questions);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(Guid id)
        {
            if (!ModelState.IsValid) BadRequest();

            var question = await this.questionRepository.GetById(id);
            return Ok(question);
        }
        [Authorize(Roles = ("Editor, Super-Admin, Admin"))]
        [HttpPut]
        public async Task<IActionResult> UpdateQuestions(UpdateQuestionRequest questionRequest)
        {
            if (!ModelState.IsValid) BadRequest();

            var questionToUpdate = new Question
            {
                Id = questionRequest.Id,
                QuizQuestion = questionRequest.QuizQuestion,
                OptionA = questionRequest.OptionA,
                OptionB = questionRequest.OptionB,
                OptionC = questionRequest.OptionC,
                OptionD = questionRequest.OptionD,
            };
            this.questionRepository.Update(questionToUpdate);
            await this.questionRepository.SaveChangesAsync();
            return Ok(new { status = "success", message = "Question successfully updated" });
        }

        [Authorize(Roles = ("Super-Admin, Admin"))]
        [HttpDelete]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            if (!ModelState.IsValid) BadRequest();

            await this.questionRepository.Delete(id);
            await this.questionRepository.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = ("Editor, Super-Admin, Admin"))]
        [HttpPost]
        public async Task<IActionResult> CreateQuestion(CreateQuestionRequest questionRequest)
        {

            if (!ModelState.IsValid) BadRequest();

            var questionToCreate = new Question
            {
                QuizQuestion = questionRequest.QuizQuestion,
                OptionA = questionRequest.OptionA,
                OptionB = questionRequest.OptionB,
                OptionC = questionRequest.OptionC,
                OptionD = questionRequest.OptionD,
                QuizId = questionRequest.QuizId,
                Answer = questionRequest.Answer
            };
            await this.questionRepository.Add(questionToCreate);
            await this.questionRepository.SaveChangesAsync();
            return Ok(new { status = "success", message = "Question successfully created for quiz" });
        }

    }
}