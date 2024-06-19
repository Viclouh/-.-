using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly SubjectService _subjectService;

        public SubjectController(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public IActionResult Get(string? query)
        {
            return StatusCode(200, _subjectService.Get(query));
        }

        [HttpPost]
        public IActionResult Post(string name)
        {
            return StatusCode(200, _subjectService.Post(name));
        }

        [HttpPut]
        public IActionResult Put(Subject subject)
        {
            return StatusCode(200, _subjectService.Put(subject));
        }

    }
}
