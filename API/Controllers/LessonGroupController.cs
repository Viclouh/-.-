using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonGroupController : ControllerBase
    {
        private readonly LessonGroupService _lessonGroupService;
        public LessonGroupController(LessonGroupService lessonGroupService)
        {
            _lessonGroupService = lessonGroupService;
        }
        [HttpGet]
        public IActionResult Get(int? groupId, int? subjectId)
        {
            return StatusCode(200, _lessonGroupService.Get(groupId, subjectId));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return StatusCode(200, _lessonGroupService.Delete(id));
        }
    }
}
