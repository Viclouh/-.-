using API.Models;

using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class LessonPlanService 
    {
        private Database.Context _context;
        public LessonPlanService(Database.Context context)
        {
            _context = context;
        }
        public IEnumerable<LessonPlan> GetAll()
        {
            return _context.LessonPlan
                .Include(ls => ls.Subject)
                .Include(ls => ls.Audience)
                .Include(ls => ls.Group)
                .Include(ls => ls.LessonTeachers);
        }

    }
}
