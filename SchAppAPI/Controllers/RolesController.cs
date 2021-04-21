using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchAppAPI.Models;

namespace SchAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [Route("create_role")]
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
    }
}
