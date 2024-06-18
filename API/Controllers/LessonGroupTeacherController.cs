using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LessonGroupTeacherController : ControllerBase
    {
        private readonly LessonGroupTeacherService _lessonGroupTeacherService;
        public LessonGroupTeacherController(LessonGroupTeacherService lessonGroupTeacherService)
        {
            _lessonGroupTeacherService = lessonGroupTeacherService;
        }
        [HttpGet]
        public IActionResult Get(int? teacherId, int? groupId, int? subjectId)
        {
            return StatusCode(200, _lessonGroupTeacherService.Get(groupId, subjectId, teacherId));
        }

    }
}
