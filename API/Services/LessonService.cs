using API.DTO;
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

        public int Delete(int id)
        {
            var item = _context.Lessons.FirstOrDefault(l => l.Id == id);
            if (item == null)
            {
                return 0;
            }

            _context.Lessons.Remove(item);
            _context.SaveChanges();
            return id;
        }

        public Lesson Post(LessonDTO lesson, Schedule schedule, List<dynamic> teachers)
        {
            LessonGroup lessonGroup;
            if (_context.LessonGroups
                .Include(lg => lg.Group)
                .Include(lg => lg.Subject)
                .Any(lg => lg.SubjectId == lesson.Subject.Id && lg.Group.Id == lesson.Group.Id && lg.ScheduleType == "1"))
            {
                lessonGroup = _context.LessonGroups
                .Include(lg => lg.Group)
                .Include(lg => lg.Subject)
                .FirstOrDefault(lg => lg.SubjectId == lesson.Subject.Id && lg.Group.Id == lesson.Group.Id && lg.ScheduleType == "1");
            }
            else
            {
                lessonGroup = new LessonGroup()
                {
                    GroupId = lesson.Group.Id,
                    SubjectId = lesson.Subject.Id,
                    ScheduleType = "1"
                };
                _context.LessonGroups.Add(lessonGroup);
                _context.SaveChanges();
            }

            var newLesson = new Lesson
            {
                LessonNumber = lesson.LessonNumber,
                DayOfWeek = lesson.Weekday,
                IsRemote = lesson.isDistantce,
                WeekOrderNumber = lesson.WeekNumber,
                ClassroomId = lesson.Audience != null ? lesson.Audience.Id : null,
                ScheduleId = schedule.Id,
                LessonGroup = lessonGroup
            };

            _context.Lessons.Add(newLesson);

            foreach (var teacher in teachers)
            {
                var lessonGroupTeacher = new LessonGroupTeacher()
                {
                    TeacherId = teacher.Id,
                    LessonGroup = lessonGroup,
                    Subgroup = (teachers.IndexOf(teacher) == 2 && teacher.IsMain) ? 2 : 1,
                    IsMain = teacher.IsMain
                };
                if (!_context.LessonGroupTeachers
                    .Any(lgt => lgt.TeacherId == lessonGroupTeacher.TeacherId 
                    && lgt.LessonGroup == lessonGroupTeacher.LessonGroup
                    && lgt.Subgroup == lessonGroupTeacher.Subgroup
                    && lgt.IsMain == lessonGroupTeacher.IsMain))
                {
                    _context.LessonGroupTeachers.Add(lessonGroupTeacher);
                }
            }

            _context.SaveChanges();

            return newLesson;
        }
    }
}
