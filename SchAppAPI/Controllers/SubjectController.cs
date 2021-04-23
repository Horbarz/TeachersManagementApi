using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchAppAPI.DOA.Requests;
using SchAppAPI.Models;
using SchAppAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SchAppAPI.DOA.Responses.SubjectResponses;

namespace SchAppAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        public readonly ISubjectRepository subjectRepo;
        public SubjectController(ISubjectRepository subjectRepo)
        {
            this.subjectRepo = subjectRepo;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await this.subjectRepo.GetAll();
            return Ok(subjects);
        }
        [HttpGet]
        public async Task<IActionResult> GetSubject(int id)
        {
            bool isValidId = Guid.TryParse(id.ToString(), out Guid idGuid);
            if (!isValidId) return BadRequest("Invalid Id");
            var subjects = await this.subjectRepo.GetById(idGuid);
            return Ok(subjects);
        }

        [HttpPut]
        public IActionResult UpdateSubject(UpdateSubjectRequest subjectRequest)
        {
            var subjectToUpdate = new Subject
            {
                Id = subjectRequest.Id,
                Name = subjectRequest.Name
            };
            this.subjectRepo.Update(subjectToUpdate);
            this.subjectRepo.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            bool isValidId = Guid.TryParse(id.ToString(), out Guid idGuid);
            if (!isValidId) return BadRequest("Invalid Id");
            await this.subjectRepo.Delete(idGuid);
            await this.subjectRepo.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GetSubject(CreateSubjectRequest subjectRequest)
        {
            var subjectToCreate = new Subject
            {
                Name = subjectRequest.Name
            };
            await this.subjectRepo.Add(subjectToCreate);
            await this.subjectRepo.SaveChangesAsync();
            return Ok();
        }
    }
}
