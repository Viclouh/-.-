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
        public IEnumerable<GroupTeacher> Get()
        {
            return _context.GroupTeacher
                .Include(gt => gt.Group)
                .Include(gt => gt.Teacher)
                .Include(gt => gt.Subject);
        }
        public IEnumerable<GroupTeacher> Get(int? groupId, int? teacherId)
        {
            if (groupId != null && teacherId != null)
            {
                return _context.GroupTeacher
                    .Include(gt => gt.Group)
                    .Include(gt => gt.Teacher)
                    .Include(gt => gt.Subject)
                    .Where(gt => gt.Group.Id == groupId && gt.Teacher.Id == teacherId);
            }
            if (groupId != null)
            {
                return _context.GroupTeacher
                    .Include(gt => gt.Group)
                    .Include(gt => gt.Teacher)
                    .Include(gt => gt.Subject)
                    .Where(gt => gt.Group.Id == groupId);
            }
            if (teacherId != null)
            {
                return _context.GroupTeacher
                    .Include(gt => gt.Group)
                    .Include(gt => gt.Teacher)
                    .Include(gt => gt.Subject)
                    .Where(gt => gt.Teacher.Id == teacherId);
            }

            return _context.GroupTeacher
                .Include(gt => gt.Group)
                .Include(gt => gt.Teacher)
                .Include(gt => gt.Subject);

        }
    }
}
