using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchAppAPI.DOA.Requests;
using SchAppAPI.Models;
using SchAppAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchAppAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        public readonly IClassRepository classRepo;
        public ClassController(IClassRepository classRepo)
        {
            this.classRepo = classRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            var subjects = await this.classRepo.GetAll();
            return Ok(subjects);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClass(Guid id)
        {
            if (!ModelState.IsValid) BadRequest();

            var subjects = await this.classRepo.GetById(id);
            return Ok(subjects);
        }
        [Authorize(Roles = ("Super-Admin, Admin"))]
        [HttpPut]
        public async Task<IActionResult> UpdateClass(UpdateClassRequest classRequest)
        {
            if (!ModelState.IsValid) BadRequest();

            var classToUpdate = new Class
            {
                Id = classRequest.Id,
                Name = classRequest.Name,
                Terms = classRequest.Terms
            };
            this.classRepo.Update(classToUpdate);
            await this.classRepo.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = ("Super-Admin, Admin"))]
        [HttpDelete]
        public async Task<IActionResult> DeleteClass(Guid id)
        {
            if (!ModelState.IsValid) BadRequest();

            await this.classRepo.Delete(id);
            await this.classRepo.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = ("Super-Admin, Admin"))]
        [HttpPost]
        public async Task<IActionResult> CreateClass(CreateClassRequest classRequest)
        {
            if (!ModelState.IsValid) BadRequest();

            var classToCreate = new Class
            {
                Name = classRequest.Name,
                Terms = classRequest.Terms
            };
            await this.classRepo.Add(classToCreate);
            await this.classRepo.SaveChangesAsync();
            return Ok();
        } 
    }
}
