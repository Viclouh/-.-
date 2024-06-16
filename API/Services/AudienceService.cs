using API.Models;

namespace API.Services
{
    public class AudienceService
    {
        private readonly Database.Context _context;

        public AudienceService(Database.Context context)
        {
            _context = context;
        }

        public List<Audience> GetAll()
        {
            return _context.Audience.ToList();
        }
    }
}
