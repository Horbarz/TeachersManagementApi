using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SchAppAPI.Contexts;
using SchAppAPI.Models;

namespace SchAppAPI.Repository
{
    public class UserRepository:IUserRepository
    {

        private readonly SchoolDbContext context;
        private readonly UserManager<User> userManager;

        public UserRepository(SchoolDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<List<User>> GetAllTeachers()
        {
            using (context)
            {
                var teachers = userManager.GetUsersInRoleAsync(UserRoles.User.ToString()).Result;
                return teachers.OfType<User>().ToList();
            }
        }
    }
}
