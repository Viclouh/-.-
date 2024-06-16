using API.Database;
using API.Models;
using API.DTO;
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
        public IEnumerable<LessonGroupTeacher> Get(int groupId)
        {
            return _context.LessonGroupTeachers
                .Include(lgt => lgt.LessonGroup.Group)
                .Include(lgt => lgt.Teacher)
                .Include(lgt => lgt.LessonGroup.Subject)
                .Where(lgt => lgt.LessonGroup.GroupId == groupId);
        }
    }
}
