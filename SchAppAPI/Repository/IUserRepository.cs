using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchAppAPI.Models;

namespace SchAppAPI.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllTeachers();
        Task<User> GetSingleTeacher(string uid);

        List<string> GetAllTokens();
    }
}
