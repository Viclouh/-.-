using Microsoft.EntityFrameworkCore;
using API.Models;
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
			return _context.Groups;
		}

        public List<Group> Get(string? query)
        {
            if (query.IsNullOrEmpty())
            {
                return GetAll().OrderBy(g => g.GroupCode).ThenBy(g => g.GroupCode).ToList();
            }

            return GetAll().Where(g => g.GroupCode.ToLower().Contains(query.ToLower()))
                .ToList();
        }
    }
}
