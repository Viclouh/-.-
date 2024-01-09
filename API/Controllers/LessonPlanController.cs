using API.DTO;
using API.Services;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
