using API.Database;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class GroupTeacherService
    {
        private Context _context;

        public GroupTeacherService(Context context)
        {
            _context = context;
        }
        public IEnumerable<GroupTeacher> Get(int groupId)
        {
            return _context.GroupTeacher
                .Include(gt => gt.Group)
                .Include(gt => gt.Teacher)
                .Include(gt => gt.Subject)
                .Where(gt => gt.Group.Id == groupId);
        }
    }
}
