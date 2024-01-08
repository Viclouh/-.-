using API.DTO;
using API.Models;

using AutoMapper;

namespace API
{
    public class AppMappingProfile: Profile
    {
        public AppMappingProfile()
        {
            CreateMap<LessonPlan, LessonPlanDTO>().ReverseMap();
        }
    }
}
