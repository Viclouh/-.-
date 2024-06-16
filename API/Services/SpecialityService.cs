using API.Models;
using Microsoft.VisualBasic.FileIO;

namespace API.Services
{
	public class SpecialityService
	{
		private Database.Context _context;
		public SpecialityService(Database.Context context)
		{
			_context = context;
		}
		public IEnumerable<Speciality> GetAll()
		{
			return _context.Speciality;
		}
	}
}
