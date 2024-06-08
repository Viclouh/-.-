
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace API.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.CustomKey) || string.IsNullOrEmpty(request.Message))
            {
                return BadRequest("Missing customKey or message");
            }

            var notificationContent = new
            {
                app_id = "915cc389-bc1e-403d-8f15-86d7dbc0463e",
                included_segments = new[] { "All" },
                data = new { customKey = request.CustomKey },
                contents = new { en = request.Message }
            };

            var content = new StringContent(JsonConvert.SerializeObject(notificationContent), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", "Basic \u003cNjkzM2ViNWUtMGViNC00MWUxLWI1YjItMzViOGZhZjMyYzE4\u003e");

            var response = await client.PostAsync("https://onesignal.com/api/v1/notifications", content);
            if (response.IsSuccessStatusCode)
            {
                return Ok("Notification sent successfully");
            }

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
