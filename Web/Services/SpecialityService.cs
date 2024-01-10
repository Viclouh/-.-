using Web.Helpers;
using Web.Models;
using Web.Services.Interfaces;

namespace Web.Services
{
	public class SpecialityService : ISpecialityService
	{
		private readonly HttpClient _client;
		public const string BasePath = "/api/speciality";

		public SpecialityService(HttpClient client)
		{
			_client = client ?? throw new ArgumentNullException(nameof(client));
		}

		public async Task<IEnumerable<Speciality>> Find()
		{
			var response = await _client.GetAsync(BasePath);

			return await response.ReadContentAsync<List<Speciality>>();
		}
	}
}
