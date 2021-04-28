using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SchAppAPI.Contexts;
using SchAppAPI.Models;

namespace SchAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly SchoolDbContext context;


        public RolesController(RoleManager<IdentityRole> roleManager, SchoolDbContext context)
        {
            this.roleManager = roleManager;
            this.context = context;
        }


        [HttpPost]
        public async Task<IActionResult> CreateRoles(Roles rolesModel)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = rolesModel.UserRoles
            };
            var result = await roleManager.CreateAsync(identityRole);

            return Ok(new { status = "success", role = rolesModel.UserRoles });
        }

        // public async Task<IActionResult> GetRoles(){
        //     var roleStore = new RoleStore<IdentityRole>(context);
        //     // var roleMngr = new RoleManager<IdentityRole>(roleStore); 

        //     // var roles = roleMngr.Roles.ToList();

        // }
    }
}
