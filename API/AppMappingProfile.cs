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

            CreateMap<LessonPlan, LessonPlanDTO>()
                .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.LessonTeachers.Select(i => i.Teacher)))
                .ForMember(dest => dest.Audience, opt => opt.MapFrom(src => src.Audience))
                .ReverseMap();
        }
    }
}
