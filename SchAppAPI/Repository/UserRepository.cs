using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SchAppAPI.Contexts;
using SchAppAPI.Models;

namespace SchAppAPI.Repository
{
    public class UserRepository : IUserRepository
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

        public List<string> GetAllTokens()
        {
            var registrationTokens = new List<string>();

            //get all teachers token and store in a list
            var teachers = userManager.GetUsersInRoleAsync(UserRoles.User.ToString()).Result;
            teachers = teachers.OfType<User>().ToList();
            foreach (var u in teachers)
            {
                var teacherToken = u.DeviceId;
                if (teacherToken != null && teacherToken.Length > 100)
                {
                    registrationTokens.Add(teacherToken);
                }
            }
            return registrationTokens;
        }

        public async Task<User> GetSingleTeacher(string uid)
        {
            if (String.IsNullOrEmpty(uid))
            {
                throw new ArgumentNullException(nameof(uid));
            }
            var res = await userManager.FindByIdAsync(uid);
            return res;
        }
    }
}
