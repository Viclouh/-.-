using API.DTO;
using API.Services;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonPlanController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly LessonPlanService _lessonPlanService;

        public LessonPlanController(LessonPlanService lessonPlanService, IMapper mapper)
        {
            _lessonPlanService = lessonPlanService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll(string formatting = "Standard") {
            switch (formatting)
            {
                case "Standard":
                    {
                        List<LessonPlanDTO> lessons = new List<LessonPlanDTO>();
                        foreach (var item in _lessonPlanService.GetAll())
                        {
                            lessons.Add(_mapper.Map<LessonPlanDTO>(item));
                        }
                        return StatusCode(200, lessons);
                        break;
                    }
                case "MobileApp":
                    {
                        List<LessonPlanForMobileDTO> lessons = new List<LessonPlanForMobileDTO>();
                        foreach (var item in _lessonPlanService.GetAll())
                        {
                            lessons.Add(_mapper.Map<LessonPlanForMobileDTO>(item));
                        }
                        return StatusCode(200, lessons);
                        break;
                    }
            }
            return StatusCode(400);
            
        }

		[HttpGet("WithParams")]
		public IActionResult GetByParameters(
            [FromQuery][Required] int weekday,
			[FromQuery][Required] int groupId,
			[FromQuery][Required] int weekNumber,
			[FromQuery][Required] int lessonNumber)
		{
            LessonPlanDTO lesson = _mapper.Map<LessonPlanDTO>(_lessonPlanService.GetByParameters(weekday, groupId, weekNumber, lessonNumber));
            if(lesson == null || weekday<1 || weekday>7 ||lessonNumber < 1||lessonNumber>6 || weekNumber > 1 || weekNumber < 0)
            {
                return StatusCode(400);
            }
            return StatusCode(200, lesson);
		}
	}

        [HttpGet]
        [Route("Search")]
        public IActionResult Search(int? teacherId, int? groupId, int? audienceId)
        {
            IEnumerable<LessonPlanDTO> lessons = _lessonPlanService.Search(teacherId, groupId, audienceId).Select(item => _mapper.Map<LessonPlanDTO>(item));
            return StatusCode(200, lessons);
        }
    }
}
