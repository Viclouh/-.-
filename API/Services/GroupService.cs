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

		public Group Post(Group group)
		{
			_context.Groups.Add(group);
			_context.SaveChanges();
			return group;
		}

		public int Delete(int id)
		{
			_context.Groups.Remove(_context.Groups.First(g => g.Id == id));
			_context.SaveChanges();
			return id;
		}

        public Group Put(Group group)
        {
            _context.Groups.Update(group);
            _context.SaveChanges();
            return group;
        }
    }
}
