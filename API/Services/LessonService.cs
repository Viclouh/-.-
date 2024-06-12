using API.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class LessonService 
    {
        private Database.Context _context;
        public LessonService(Database.Context context)
        {
            _context = context;
        }
        private IQueryable<Lesson> GetAllWithIncludes()
        {
            return _context.Lessons
                .Include(l => l.LessonGroup)
                    .ThenInclude(lg => lg.LessonGroupTeachers)
                    .ThenInclude(lgt => lgt.Teacher)
                .Include(l => l.LessonGroup)
                    .ThenInclude(lg => lg.Group)
                .Include(l => l.LessonGroup)
                    .ThenInclude(lg => lg.Subject)
                .Include(ls => ls.Classroom)
                    .ThenInclude(r => r.ClassroomType);
        }

        public IEnumerable<Lesson> GetAll()
        {
            return GetAllWithIncludes();
        }
        public IEnumerable<Lesson> Search(int? teacherId, int? groupId, int? classroom)
        {
            IQueryable<Lesson> query = GetAllWithIncludes()
                .AsNoTracking();

            if (teacherId.HasValue)
            {
                query = query.Where(item => item.LessonGroup.LessonGroupTeachers.Any(lgt=> lgt.TeacherId == teacherId.Value))
                    .OrderByDescending(l => l.LessonGroup.LessonGroupTeachers.Any(lgt => lgt.TeacherId == teacherId.Value && lgt.IsMain));
            }

            if (groupId.HasValue)
            {
                query = query.Where(item => item.LessonGroup.GroupId== groupId.Value);
            }

            if (classroom.HasValue)
            {
                query = query.Where(item => item.ClassroomId == classroom.Value);
            }

            return query.ToList();
        }

        public Lesson GetByParameters(int weekday, int groupId, int weekNumber, int lessonNumber)
        {
            Lesson lesson = GetAllWithIncludes()
                .Where(l => l.DayOfWeek == weekday
                && l.LessonGroup.GroupId == groupId
                && l.WeekOrderNumber == weekNumber
                && l.LessonNumber == lessonNumber)
                .FirstOrDefault();
            if (lesson != null)
            {
                return lesson;
            }
            if(_context.Groups.Where(g => g.Id == groupId).FirstOrDefault()!=null)
            {
                return new Lesson
                {
                    DayOfWeek = weekday,
                    WeekOrderNumber = weekNumber,
                    LessonNumber = lessonNumber,
                    LessonGroup = new LessonGroup()
                    {
                        GroupId = _context.Groups.Where(g => g.Id == groupId ).FirstOrDefault().Id
                    }
                };
            }
            return null;
		}
    }
}
