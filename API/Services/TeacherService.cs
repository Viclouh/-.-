using API.Database;
using API.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TeacherService
    {
        Database.Context _context;

        public TeacherService(Context context)
        {
            _context = context;
        }
        public List<Teacher> GetAll()
        {
            return _context.Teacher.ToList();
        }
        public Teacher Get(int id)
        {
            return _context.Teacher.Where(t => t.Id == id).FirstOrDefault();
        }
        public List<Teacher> Get(string? query)
        {
            if (query.IsNullOrEmpty())
            {
                return _context.Teacher.ToList();
            }

            return _context.Teacher.Where(t => t.Surname.ToLower().Contains(query.ToLower())
            || t.Name.ToLower().Contains(query.ToLower())
            || t.Patronymic.ToLower().Contains(query.ToLower())).ToList();
        }
    }
}
