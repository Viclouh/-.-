using Microsoft.EntityFrameworkCore;

namespace API.Services
{
	public class GroupService
	{
		private Database.Context _context;
		public GroupService(Database.Context context)
		{
			_context = context;
		}
		public IEnumerable<Models.Group> GetAll() 
		{
			return _context.Group.Include(x=>x.Speciality);
		}
	}
}
