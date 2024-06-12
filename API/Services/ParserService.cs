
namespace API.Services
{
    public class ParserService
    {
        private Database.Context _context;

        public ParserService(Database.Context context)
        {
            _context = context;
        }
        public void Parse(int year, int semester, int status, int department)
        {

        }
    }
}
