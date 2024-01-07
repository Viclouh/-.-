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
        private readonly LessonPlanService _planLessonService = new LessonPlanService();

        [HttpGet]
        public LessonPlanDTO GetAll() {
        return _mapper.Map<LessonPlanDTO>(LessonPlan)
        }
    }
}
