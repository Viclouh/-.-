using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API.Models;

namespace API.Services
{
    public class NotificationService
    {
        private static readonly HttpClient client = new HttpClient();

        public NotificationService()
        {
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", "Basic NjkzM2ViNWUtMGViNC00MWUxLWI1YjItMzViOGZhZjMyYzE4");
        }

        public async Task<HttpResponseMessage> SendNotificationAsync(string message, string tagKey, string tagValue)
        {
            var notificationContent = new
            {
                app_id = "915cc389-bc1e-403d-8f15-86d7dbc0463e",
                contents = new { en = message },
                filters = new[] { new { field = "tag", key = tagKey, relation = "=", value = tagValue } }
            };

            var content = new StringContent(JsonConvert.SerializeObject(notificationContent), Encoding.UTF8, "application/json");
            return await client.PostAsync("https://onesignal.com/api/v1/notifications", content);
        }
        public string GetScheduleChangeMessage(int weekNumber, int weekday, DateTime? date = null)
        {
            // Проверяем, предоставлена ли дата
            if (date.HasValue)
            {
                // Формируем сообщение с использованием даты
                return $"У вас изменение в расписании на {date.Value:dd.MM.yyyy}.";
            }

            // Словарь для соответствия номера дня недели его названию на русском языке
            var dayNames = new Dictionary<int, string>
        {
            { 1, "понедельник" },
            { 2, "вторник" },
            { 3, "среда" },
            { 4, "четверг" },
            { 5, "пятница" },
            { 6, "суббота" },
            { 7, "воскресенье" }
        };

            if (weekday < 1 || weekday > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(weekday), "День недели должен быть в диапазоне от 1 до 7.");
            }

            // Получаем название дня недели
            string dayName = dayNames[weekday];

            // Формируем сообщение
            return $"У вас изменение в расписании. В {dayName} {weekNumber} недели.";
        }
    }
}