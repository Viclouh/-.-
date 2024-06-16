using API.DTO;
using API.Models;

using AutoMapper;

namespace API
{
    public class AppMappingProfile: Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Classroom, ClassroomDTO>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src=>src.ClassroomType.Name));

            CreateMap<Lesson, LessonDTO>()
                .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.LessonGroup.LessonGroupTeachers.Select(i => i.Teacher)))
                .ForMember(dest => dest.Audience, opt => opt.MapFrom(src => src.Classroom))
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.LessonGroup.Group))
                .ForMember(dest => dest.WeekNumber, opt => opt.MapFrom(src => src.WeekOrderNumber))
                .ForMember(dest => dest.Weekday, opt => opt.MapFrom(src => src.DayOfWeek))
                .ForMember(dest => dest.isDistantce, opt => opt.MapFrom(src => src.IsRemote))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.LessonGroup.Subject))
                .ReverseMap();

            CreateMap<Lesson, LessonForMobileDTO>()
                .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.LessonGroup.LessonGroupTeachers.Select(i => i.Teacher)))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.LessonGroup.Subject.Name))
                .ForMember(dest => dest.ShortSubjectName, opt => opt.MapFrom(src => src.LessonGroup.Subject.ShortName))
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.LessonGroup.Group.GroupCode))
                .ForMember(dest => dest.Audience, opt => opt.MapFrom(src => src.Classroom.Number))
                .ReverseMap();
        }
    }
}
