using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchAppAPI.DOA;
using SchAppAPI.Models;
using SchAppAPI.Repository;
using SchAppAPI.Services;

namespace SchAppAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {

        public readonly IUserRepository userRepository;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<TeachersController> logger;
        private readonly IMapper _mapper;

        private Teacher Teacher { get; set; } = new Teacher();


        public TeachersController(IUserRepository userRepository, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, ILogger<TeachersController> logger, IMapper _mapper)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
            this._mapper = _mapper;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<List<ResponseTeacher>> GetAllTeachers()
        {

            var allTeachers = await userRepository.GetAllTeachers();
            var teachersToReturn = _mapper.Map<List<ResponseTeacher>>(allTeachers);
            return teachersToReturn;
        }

        [Authorize(Roles = ("Teacher"))]
        [HttpGet]
        [Route("getdetail")]
        public async Task<IActionResult> GetSingleTeacher()
        {

            var userId = User.Claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault();


            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("User not found");

            var teacherEntity = await userRepository.GetSingleTeacher(userId);


            if (teacherEntity == null)
            {
                return NotFound();
            }

            var mappedTeachers = _mapper.Map<ResponseTeacher>(teacherEntity);
            return Ok(mappedTeachers);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleTeacher(String uid)
        {
            var teacherEntity = await userRepository.GetSingleTeacher(uid);


            if (teacherEntity == null)
            {
                return NotFound();
            }

            var mappedTeachers = _mapper.Map<ResponseTeacher>(teacherEntity);
            return Ok(mappedTeachers);

        }



        [HttpPut("{uid}")]
        public async Task<IActionResult> PutSingleTeacher(String uid, EditTeacher teach)
        {
            var teacherEntity = await userManager.FindByIdAsync(uid);

            if (teacherEntity == null)
            {
                return NotFound();
            }

            var userClaims = await userManager.GetClaimsAsync(teacherEntity);
            var userRoles = await userManager.GetRolesAsync(teacherEntity);

            //var mappedTeachers = _mapper.Map(teacherEntity, teach);
            teacherEntity.FirstName = teach.FirstName;
            teacherEntity.LastName = teach.LastName;
            teacherEntity.Gender = teach.Gender;
            teacherEntity.SchoolName = teach.SchoolName;
            teacherEntity.Cateory = teach.Category;
            teacherEntity.State = teach.location.state;
            teacherEntity.City = teach.location.city;
            teacherEntity.Experience = teach.Experience;
            teacherEntity.SchoolType = teach.SchoolType;

            var result = await userManager.UpdateAsync(teacherEntity);


            if (result.Succeeded)
            {
                var mappedTeachers = _mapper.Map(teacherEntity, teach);
                return Ok(mappedTeachers);
            }

            return NoContent();


        }



        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddTeacher([FromBody] Teacher model)
        {


            string password = PasswordGenerator.GenerateRandomPassword();
            logger.LogInformation(password);
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });

            User user = new User()
            {

                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                SchoolName = model.SchoolName,
                Cateory = model.Category,
                State = model.location.state,
                City = model.location.city,
                Experience = model.Experience,
                SchoolType = model.SchoolType,

            };
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "failed", isRegistered = false, Message = result.Errors });
            }
            else
            {
                await userManager.AddToRoleAsync(user, UserRoles.User);

            }

            return Ok(new { Status = "Success", isRegistered = true, Message = "User created successfully!", Password = password });

        }

    }
}
