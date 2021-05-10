using AutoMapper;
using SchAppAPI.DOA;
using SchAppAPI.DOA.Responses;
using SchAppAPI.Models.Lesson;

namespace SchAppAPI.Services
{
    public class SchoolProfile : Profile
    {

        public SchoolProfile()
        {
            CreateMap<Models.User, DOA.Teacher>().ReverseMap();
            CreateMap<Models.User, ResponseTeacher>()
                .ForPath(dest => dest.location.city, opt => opt.MapFrom(src => src.City))
                .ForPath(dest => dest.location.state, opt => opt.MapFrom(src => src.State));
            CreateMap<Models.User, DOA.EditTeacher>().ReverseMap();
            CreateMap<LessonReport, GetDownloadedLessonResponse>()
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Lesson.Subject.Name))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Lesson.Name));
            CreateMap<LessonReport, GetRecentLessonResponse>()
                .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.Lesson.Thumbnail))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Lesson.Name));
            CreateMap<Lesson, GetAllLessonReponse>()
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class.Name));
            CreateMap<Content, GetLessonContentesponse>();
            CreateMap<Quiz, GetLessonQuizResponse>();
            CreateMap<Lesson, GetLessonReponse>()
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class.Name))
                .ForMember(dest => dest.Quiz, opt => opt.MapFrom(src => src.Quiz))
                .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents));

        }

    }
}