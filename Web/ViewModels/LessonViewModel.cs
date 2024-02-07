using Web.Models;

namespace Web.ViewModels
{
    public class LessonViewModel
    {
        public LessonPlan Lesson { get; set; }

        public IEnumerable<Teacher> Teachers { get; set; }

        public List<String> Weekdays = new List<String>
        {
            "Понедельник",
            "Вторник",
            "Среда",
            "Четверг",
            "Пятница",
            "Суббота",
        };
    }
}
