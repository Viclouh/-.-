using API.Models;

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
            return Da
        }

    }
}
