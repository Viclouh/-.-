using API.Models;
using API.Services;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherService _teacherService;
        public TeacherController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }


        // GET: api/<TeacherController>
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return StatusCode(200,_teacherService.GetAll());
        //}

        // GET api/<TeacherController>/5|
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return StatusCode(200, _teacherService.Get(id));
        }

        [HttpGet]
        public IActionResult Get(string? query)
        {
            return StatusCode(200, _teacherService.Get(query));
        }

        // POST api/<TeacherController>
        [HttpPost]
        public IActionResult Post([FromBody] Teacher value)
        {
            return StatusCode(200, _teacherService.Post(value));
        }

        // PUT api/<TeacherController>
        [HttpPut]
        public IActionResult Put([FromBody] Teacher teacher)
        {
            return StatusCode(200, _teacherService.Put(teacher));
        }

        // DELETE api/<TeacherController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return StatusCode(200, _teacherService.Delete(id));
        }
    }
}
