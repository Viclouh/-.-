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
		private const string LessonPath = "/api/LessonPlan";

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
			var response = await _client.GetAsync(LessonPath);

			return await response.ReadContentAsync<List<LessonPlan>>();
		}
	}
}
