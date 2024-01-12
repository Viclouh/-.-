using Web.Models;

namespace Web.ViewModels
{
	public class MainScheduleViewModel
	{
		public IEnumerable<Speciality> Specialities { get; set; }
		public IEnumerable<Group> Groups { get; set; }
		public IEnumerable<Teacher> Teachers { get; set; }
		public IEnumerable<LessonPlan> Lessons { get; set; }
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
