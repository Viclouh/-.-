using API.Database;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class LessonGroupService
    {
        private Context _context;


        public LessonGroupService(Context context)
        {
            _context = context;
        }

        public List<LessonGroup> GetWithIncludes()
        {
            return _context.LessonGroups
                .Include(ts => ts.Group)
                .Include(ts => ts.Subject)
                .Include(lg => lg.LessonGroupTeachers)
                    .ThenInclude(lgt => lgt.Teacher).ToList();
        }

        public List<LessonGroup> Get(int? groupId, int? subjectId)
        {
            if (groupId.HasValue && subjectId.HasValue)
            {
                return GetWithIncludes().Where(ts => ts.GroupId == groupId && ts.SubjectId == subjectId).ToList();
            }
            if (subjectId.HasValue)
            {
                return GetWithIncludes().Where(ts => ts.SubjectId == subjectId).ToList();
            }
            if (groupId.HasValue)
            {
                return GetWithIncludes().Where(ts => ts.GroupId == groupId).ToList();
            }

            return GetWithIncludes().ToList();
        }

        public int Delete(int id)
        {
            _context.LessonGroups.Remove(_context.LessonGroups.First(lg => lg.Id == id));
            return id;
        }
    }
}
