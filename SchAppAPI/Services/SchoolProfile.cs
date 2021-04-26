using AutoMapper;
using SchAppAPI.DOA;

namespace SchAppAPI.Services
{
    public class SchoolProfile : Profile
    {

        public SchoolProfile()
        {
            CreateMap<Models.User, DOA.Teacher>().ReverseMap();
            CreateMap<Models.User, DOA.EditTeacher>().ReverseMap();

        }

    }
}