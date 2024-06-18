using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Message))
            {
                return BadRequest("Сообщение отсутствует");
            }

            var response = await _notificationService.SendNotificationAsync(request.Message, request.TagKey, request.TagValue);
            if (response.IsSuccessStatusCode)
            {
                return Ok("Уведомление успешно отправлено");
            }

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        
    }
}