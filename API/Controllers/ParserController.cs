using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParserController : ControllerBase
    {
        public readonly ParserService _ParserService;

        public ParserController(ParserService ParserService)
        {
            _ParserService = ParserService;
        }

        //[HttpGet]
        //public IActionResult Parse(string base64, int year, int semester, int statusId, int department)
        //{
        //    //return StatusCode(200, _ParserService.Parse(base64, year, semester, statusId, department));
        //}
    }
}
