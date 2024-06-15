using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSubjectController : ControllerBase
    {
        private readonly TeacherSubjectService _teacherSubjectService;
        public TeacherSubjectController(TeacherSubjectService teacherSubjectService)
        {
            _teacherSubjectService = teacherSubjectService;
        }

        [HttpGet]
        public IActionResult Get(int? teacherId, int? subjectId)
        {
            return StatusCode(200, _teacherSubjectService.Get(teacherId, subjectId));
        }
    }
}
