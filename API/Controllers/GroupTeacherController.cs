using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupTeacherController : ControllerBase
    {
        private readonly GroupTeacherService _groupTeacherService;

        public GroupTeacherController(GroupTeacherService groupTeacherService)
        {
            _groupTeacherService = groupTeacherService;
        }

        [HttpGet]
        public IActionResult Get(int? groupId, int? teacherId)
        {
            return StatusCode(200, _groupTeacherService.Get(groupId, teacherId).ToList());
        }
    }
}
