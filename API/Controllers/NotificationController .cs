
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
            if (request == null || string.IsNullOrEmpty(request.Message))
            {
                return BadRequest("Missing message");
            }

            var notificationContent = new
            {
                app_id = "915cc389-bc1e-403d-8f15-86d7dbc0463e",
                contents = new { en = request.Message },
                filters = new[] { new { field = "tag", key = request.TagKey, relation = "=", value = request.TagValue } }
            };

            var content = new StringContent(JsonConvert.SerializeObject(notificationContent), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", "Basic NjkzM2ViNWUtMGViNC00MWUxLWI1YjItMzViOGZhZjMyYzE4");

            var response = await client.PostAsync("https://onesignal.com/api/v1/notifications", content);
            if (response.IsSuccessStatusCode)
            {
                return Ok("Notification sent successfully");
            }

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
