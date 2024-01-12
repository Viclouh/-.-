using Web.Models;

namespace Web.Services.Interfaces
{
	public interface IScheduleService
	{
		Task<IEnumerable<Speciality>> GetSpecialities();

		Task<IEnumerable<Group>> GetGroups();

		Task<IEnumerable<LessonPlan>> GetLessons();
	}
}
