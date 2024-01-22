using API.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudienceController : ControllerBase
    {
        private readonly AudienceService _audienceService;

        public AudienceController(AudienceService audienceService)
        {
            _audienceService = audienceService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(200, _audienceService.GetAll());
        }
    }
}
