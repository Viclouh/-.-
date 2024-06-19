using API.Services;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleService _ScheduleService;

        public ScheduleController(ScheduleService scheduleService)
        {
            _ScheduleService = scheduleService;
        }

        [HttpGet]
        public IActionResult Get() 
        { 
            return StatusCode(200, _ScheduleService.Get().ToList());
        }

        [HttpPost("active")]
        public IActionResult SetActive(int scheduleId)
        {
            try
            {
                _ScheduleService.ActivateSchedule(scheduleId);
                return StatusCode(200);
            }
            catch (InvalidDataException ex)
            {
                return StatusCode(400, ex.Message);
            }
            
        }
    }
}
