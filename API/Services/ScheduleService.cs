using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ScheduleService
    {
        private Database.Context _context;
        public ScheduleService(Database.Context context)
        {
            _context = context;
        }

        public IEnumerable<Schedule> Get()
        {
            return _context.Schedules.Include(s => s.ScheduleStatus).Include(s => s.Lessons);
        }
    }
}
