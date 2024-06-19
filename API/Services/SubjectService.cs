using API.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class SubjectService
    {
        private readonly Database.Context _context;

        public SubjectService(Database.Context context)
        {
            _context = context;
        }

        public List<Subject> Get(string? query)
        {
            if (query.IsNullOrEmpty())
            {
                return _context.Subjects.ToList();
            }

            return _context.Subjects
                .Where(s => s.Name.ToLower().Contains(query.ToLower()) && s.ShortName.ToLower().Contains(query.ToLower()))
                .ToList();
        }
        public Subject Post(string name)
        {
            var subject = new Subject
            {
                Name = name,
                ShortName = name
            };

            _context.Subjects.Add(subject);
            _context.SaveChanges();
            return subject;
        }
        public Subject Put(Subject subject)
        {
            _context.Subjects.Update(subject);
            _context.SaveChanges();

            return subject;
        }


    }
}
