using Web.Models;

namespace Web.Services.Interfaces
{
	public interface ISpecialityService
	{
		Task<IEnumerable<Speciality>> Find();
	}
}
