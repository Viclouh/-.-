using API.Models;
using API.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

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

        public bool Delete(int id)
        {
            var item  = _context.LessonPlan.Where(lp => lp.Id == id).FirstOrDefault();

            if(item == null)
            {
                return false;
            }

            _context.LessonPlan.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public LessonPlan Post(LessonPlanDTO lesson)
        {

            var newLesson = new LessonPlan
            {
                LessonNumber = lesson.LessonNumber,
                Weekday = lesson.Weekday,
                GroupId = lesson.Group.Id,
                AudienceId = lesson.Audience != null ? lesson.Audience.Id : null,
                SubjectId = lesson.Subject.Id,
                WeekNumber = lesson.WeekNumber,
            };

            _context.LessonPlan.Add(newLesson);

            _context.SaveChanges();

            newLesson = _context.LessonPlan.Where(lp => lp.LessonNumber == newLesson.LessonNumber &&
            lp.Weekday == newLesson.Weekday &&
            lp.GroupId == newLesson.GroupId &&
            lp.WeekNumber == newLesson.WeekNumber).FirstOrDefault();

            _context.LessonTeacher.Add(new LessonTeacher
            {
                Lesson = newLesson,
                TeacherId = lesson.Teachers.First().Id,
                IsGeneral = true,
            });

            if (lesson.Teachers.Last() != null)
            {
                _context.LessonTeacher.Add(new LessonTeacher
                {
                    Lesson = newLesson,
                    TeacherId = lesson.Teachers.Last().Id,
                    IsGeneral = false,
                });
            }

            _context.SaveChanges();

            return GetByParameters(lesson.Weekday, lesson.Group.Id, lesson.WeekNumber, lesson.LessonNumber);
        }

        public LessonPlan Put(LessonPlanDTO lesson)
        {
            var updatedLesson = GetByParameters(lesson.Weekday, lesson.Group.Id, lesson.WeekNumber, lesson.LessonNumber);

            var mainTeacher = updatedLesson.LessonTeachers.First();

            mainTeacher.Teacher.Id = lesson.Teachers.First().Id;

            _context.LessonTeacher.Update(mainTeacher);

            if (lesson.Teachers.Last() != lesson.Teachers.First())
            {
                if (updatedLesson.LessonTeachers.First() == updatedLesson.LessonTeachers.Last())
                {
                    _context.LessonTeacher.Add(new LessonTeacher { Lesson = updatedLesson, TeacherId =  lesson.Teachers.Last().Id, IsGeneral = false});
                }
                else
                {
                    var secondTeacher = updatedLesson.LessonTeachers.Last();
                    secondTeacher.TeacherId = lesson.Teachers.Last().Id;
                    _context.LessonTeacher.Update(secondTeacher);
                }
            }

            updatedLesson.AudienceId = lesson.Audience == null ? null : lesson.Audience.Id;
            updatedLesson.SubjectId = lesson.Subject.Id;
            _context.LessonPlan.Update(updatedLesson);

            _context.SaveChanges();

            return GetByParameters(lesson.Weekday, lesson.Group.Id, lesson.WeekNumber, lesson.LessonNumber);
        }
    }
}
