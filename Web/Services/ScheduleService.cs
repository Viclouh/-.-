using Web.Helpers;
using Web.Models;
using Web.Services.Interfaces;

namespace Web.Services
{
	public class ScheduleService : IScheduleService
	{
		private readonly HttpClient _client;
		private const string SpecialityPath = "/api/speciality";
		private const string GroupPath = "/api/group";
		private const string LessonsPath = "/api/LessonPlan";
		private const string LessonPath = "/api/LessonPlan/WithParams";

		public ScheduleService(HttpClient client)
		{
			_client = client ?? throw new ArgumentNullException(nameof(client));
		}

		public async Task<IEnumerable<Speciality>> GetSpecialities()
		{
			var response = await _client.GetAsync(SpecialityPath);

			return await response.ReadContentAsync<List<Speciality>>();
		}
		public async Task<IEnumerable<Group>> GetGroups()
		{
			var response = await _client.GetAsync(GroupPath);

			return await response.ReadContentAsync<List<Group>>();
		}

		public async Task<IEnumerable<LessonPlan>> GetLessons()
		{
			var response = await _client.GetAsync(LessonsPath);

			return await response.ReadContentAsync<List<LessonPlan>>();
		}
		public async Task<LessonPlan> GetLesson(int weekday, int lessonNumber, int groupId, int weekNumber)
		{
			var path = LessonPath + $"?weekday={weekday}&groupId={groupId}&weekNumber={weekNumber}&lessonNumber={lessonNumber}";
			var response = await _client.GetAsync(path) ;

			return await response.ReadContentAsync<LessonPlan>();
		}
	}
}
