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
        public IEnumerable<LessonPlanDTO> GetAll() {
            List<LessonPlanDTO> lessons = new List<LessonPlanDTO>();
            foreach (var item in _lessonPlanService.GetAll())
            {
                lessons.Add(_mapper.Map<LessonPlanDTO>(item));
            }
            return lessons;
        }
    }
}
