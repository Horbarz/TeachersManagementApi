using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchAppAPI.Models;
using SchAppAPI.Repository;

namespace SchAppAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController:ControllerBase
    {

        public readonly IUserRepository userRepository;

        public TeachersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        [Route("getAll")]
        public Task<List<User>> GetAllTeachers()
        {
            var allTeachers = userRepository.GetAllTeachers();
            return allTeachers;
        }


    }
}
