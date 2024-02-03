using API.Models;

namespace API.Services
{
    public class WeekService
    {
        private Database.Context _context;
        public WeekService(Database.Context context)
        {
            _context = context;
        }

        public int GetCurrentWeek()
        {
            DateTime start = _context.YearBegin.FirstOrDefault().DateStart;
            TimeSpan timeSpan = DateTime.Now - start;            
            return timeSpan.Days / 7 % 2 +1;
        }
    }
}
