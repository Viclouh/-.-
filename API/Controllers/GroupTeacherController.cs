using API.Models;
using API.DTO;
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

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (_groupTeacherService.Delete(id))
            {
                return StatusCode(200, id);
            }
            return StatusCode(400);
        }

        [HttpPost]
        public IActionResult Post([FromBody]GroupTeacherDTO groupTeacher)
        {
            return StatusCode(200, _groupTeacherService.Post(groupTeacher));
        }
    }
}
