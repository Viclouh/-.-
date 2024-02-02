using API.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class LessonPlanService 
    {
        private Database.Context _context;
        public LessonPlanService(Database.Context context)
        {
            _context = context;
        }
        public IEnumerable<LessonPlan> GetAll()
        {
            return _context.LessonPlan
                .Include(ls => ls.Subject)
                .Include(ls => ls.Audience)
                    .ThenInclude(a => a.AudienceType)
                .Include(ls => ls.Group)
                    .ThenInclude(g=>g.Speciality)
                .Include(ls => ls.LessonTeachers)
                    .ThenInclude(lt => lt.Teacher);
        }
        public IEnumerable<LessonPlan> Search(int? teacherId, int? groupId, int? audienceId)
        {
            IQueryable<LessonPlan> query = _context.LessonPlan.Include(lp => lp.LessonTeachers).ThenInclude(lt=>lt.Teacher).Include(lp => lp.Audience).Include(lp => lp.Group).ThenInclude(g=>g.Speciality).Include(lp=>lp.Subject).AsNoTracking();

            if (teacherId.HasValue)
            {
                query = query.Where(item => item.LessonTeachers.Any(lt=>lt.TeacherId == teacherId.Value))
                    .OrderByDescending(lp=>lp.LessonTeachers.FirstOrDefault(lt => lt.TeacherId == teacherId.Value).IsGeneral);
            }

            if (groupId.HasValue)
            {
                query = query.Where(item => item.GroupId == groupId.Value);
            }

            if (audienceId.HasValue)
            {
                query = query.Where(item => item.AudienceId == audienceId.Value);
            }

            return query.ToList();
        }

        public LessonPlan GetByParameters(int weekday, int groupId, int weekNumber, int lessonNumber)
        {
            LessonPlan lesson = _context.LessonPlan
                .Include(ls => ls.Subject)
                .Include(ls => ls.Audience)
                    .ThenInclude(a => a.AudienceType)
                .Include(ls => ls.Group)
                    .ThenInclude(g => g.Speciality)
                .Include(ls => ls.LessonTeachers)
                    .ThenInclude(lt => lt.Teacher)
                .Where(ls => ls.Weekday == weekday
                && ls.Group.Id == groupId
                && ls.WeekNumber == weekNumber
                && ls.LessonNumber == lessonNumber)
                .FirstOrDefault();
            if (lesson != null)
            {
                return lesson;
            }
            if(_context.Group.Where(g => g.Id == groupId).FirstOrDefault()!=null)
            {
                return new LessonPlan
                {
                    Weekday = weekday,
                    WeekNumber = weekNumber,
                    LessonNumber = lessonNumber,
                    Group = _context.Group.Include(g=>g.Speciality).Where(g => g.Id == groupId).FirstOrDefault()
                };
            }
            return null;
		}
    }
}
