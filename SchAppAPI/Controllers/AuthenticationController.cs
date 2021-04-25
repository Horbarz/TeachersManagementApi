using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SchAppAPI.Contexts;
using SchAppAPI.DOA;
using SchAppAPI.Models;
using SchAppAPI.Repository;
using SchAppAPI.Services;

namespace SchAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> userManager;

        private readonly IMailService mailService;

        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<AuthenticateController> logger;
        private readonly IConfiguration _configuration;
        private readonly SchoolDbContext _context;


        public AuthenticateController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            SchoolDbContext context,
            SignInManager<User> signInManager,
            ILogger<AuthenticateController> logger,
            IMailService mailService

        )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._configuration = configuration;
            this._context = context;
            this.logger = logger;
            this.signInManager = signInManager;
            this.mailService = mailService;

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {

            //get the user by email
            var user = await userManager.FindByNameAsync(model.Email);


            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
            //logger.LogInformation(result.ToString());

            //await userManager.CheckPasswordAsync(user, model.Password)
            //check if user exists 
            if (user != null && result.Succeeded)
            {
                //check the user role
                //pass data into claims
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>{
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim("Id",user.Id),

                };

                //Add new claim
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                //create signing key
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
                return Ok(new
                {
                    isLoggedIn = true,
                    message = "Operation successful",
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                    // expiration = token.ValidTo,
                    // role = userRoles,
                    // email = user.UserName,
                    // userId = user.Id
                });

            }
            //if (result.IsLockedOut)
            //{
            //    return Ok(new
            //    {
            //        isLocked = true
            //    }
            //    );
            //}

            return Ok(
                new
                {
                    Status = "Error",
                    isLoggedIn = false,
                    Message = "Invalid Username or Password"
                }
            );

        }

        [HttpPost]
        [Route("register_user")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
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
                Cateory = model.Cateory,
                State = model.State,
                City = model.City,
                Experience = model.Experience,
                SchoolType = model.SchoolType,

            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "failed", isRegistered = false, Message = result.Errors });
            }
            else
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                await userManager.AddToRoleAsync(user, UserRoles.User);

            }

            return Ok(new { Status = "Success", isRegistered = true, Message = "User created successfully!" });
        }


        [HttpPost]
        [Route("register_admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdmin model)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "failed", Message = "User already exists!" });
            }
            User user = new User()
            {
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),

            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "failed", Message = result.Errors });
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new { Status = "Success", Message = "User created successfully!" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("forgot_password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            var userExists = await userManager.FindByNameAsync(forgotPassword.Email);
            if (userExists == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "failed", Message = "User does not exists!" });
            }

            //Generate password reset token
            var token = await userManager.GeneratePasswordResetTokenAsync(userExists);
            var passwordResetLink = Url.Action("ResetPassword", "AuthenticationController",
                    new { email = forgotPassword.Email, token }, Request.Scheme);
            //Send to email         
            return Ok(new { reseturl = passwordResetLink });

        }

        [HttpPost]
        [Route("reset_password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword reset)
        {
            var user = await userManager.FindByNameAsync(reset.Email);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "failed", Message = "User does not exists!" });
            }
            var resetPasswordResult = await userManager.ResetPasswordAsync(user, reset.Token, reset.Password);
            if (!resetPasswordResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "failed", Message = resetPasswordResult.Errors });
            }
            return Ok(new { Status = "Success", Message = "Password was reset successfully" });

        }
    }
}
