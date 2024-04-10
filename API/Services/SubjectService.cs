using API.Models;

namespace API.Services
{
    public class SubjectService
    {
        private readonly Database.Context _context;

        public SubjectService(Database.Context context)
        {
            _context = context;
        }

        public List<Subject> GetAll()
        {
            return _context.Subject.ToList();
        }
    }
}
