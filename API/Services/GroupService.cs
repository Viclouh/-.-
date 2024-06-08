using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

		public List<Group> Get(string? query)
		{
			if (query.IsNullOrEmpty())
			{
				return GetAll().OrderBy(g => g.Speciality.Name).ThenBy(g => g.Name).ToList();
			}

			return GetAll().Where(g => g.Speciality.Name.ToLower().Contains(query.ToLower())
				|| g.Name.Contains(query.ToLower()) || g.Speciality.Shortname.ToLower().Contains(query.ToLower()))
				.ToList();
		}
	}
}
