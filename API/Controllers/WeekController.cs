using API.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeekController : ControllerBase
    {
        private WeekService _weekService;

        public WeekController(WeekService weekService)
        {
            _weekService = weekService;
        }

        [HttpGet]
        [Route("/Current")]
        public IActionResult Get() {
            return StatusCode(200, _weekService.GetCurrentWeek());
        }
        [HttpPost]
        public IActionResult Set(DateTime startDate) {
            _weekService.SetYearStart(startDate);
            return StatusCode(200,"Year start updated");
        }
    }
}
