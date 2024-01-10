using API.DTO;
using API.Models;

using AutoMapper;

namespace API
{
    public class AppMappingProfile: Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Audience, AudienceDTO>();

            CreateMap<Speciality, SpecialityDTO>();

            CreateMap<LessonPlan, LessonPlanDTO>()
                .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.LessonTeachers.Select(i => i.Teacher)))
                .ForMember(dest => dest.Audience, opt => opt.MapFrom(src => src.Audience))
                .ReverseMap();

            CreateMap<LessonPlan, LessonPlanForMobileDTO>()
                .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.LessonTeachers.Select(i => i.Teacher)))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(dest => dest.ShortSubjectName, opt => opt.MapFrom(src => src.Subject.Shortname))
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group.Speciality.Shortname + " - " + src.Group.Name))
                .ForMember(dest => dest.Audiebce, opt => opt.MapFrom(src => src.Audience.Number))
                .ReverseMap();
        }
    }
}
