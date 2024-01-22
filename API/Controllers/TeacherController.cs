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
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(200,_teacherService.GetAll());
        }

        // GET api/<TeacherController>/5|
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TeacherController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TeacherController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TeacherController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
