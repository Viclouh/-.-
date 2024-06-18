using API.Database;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace API.Services
{
    public class LessonGroupTeacherService
    {
        private Context _context;

        public LessonGroupTeacherService(Context context)
        {
            _context = context;
        }

        public List<LessonGroupTeacher> GetWithIncludes()
        {
            return _context.LessonGroupTeachers
                .Include(lgt => lgt.Teacher)
                .Include(lgt => lgt.LessonGroup)
                    .ThenInclude(lg => lg.Group)
                .Include(lgt => lgt.LessonGroup)
                    .ThenInclude(lg => lg.Subject).ToList();
        }

        public List<LessonGroupTeacher> Get(int? groupId, int? subjectId, int? teacherId)
        {
            List<LessonGroupTeacher> query = GetWithIncludes();
            if (groupId.HasValue)
            {
                query = query.Where(lgt => lgt.LessonGroup.Group.Id == groupId).ToList();
            }
            if (subjectId.HasValue)
            {
                query = query.Where(lgt => lgt.LessonGroup.Subject.Id == subjectId).ToList();
            }
            if (teacherId.HasValue)
            {
                query = query.Where(lgt => lgt.Teacher.Id == teacherId).ToList();
            }
            return query;
        }

        public int Delete(int id)
        {
            _context.LessonGroupTeachers.Remove(_context.LessonGroupTeachers.First(lg => lg.Id == id));
            _context.SaveChanges();
            return id;
        }

        public LessonGroupTeacher Post(int teacherId, int lessonGroupId)
        {
            var newLGT = new LessonGroupTeacher
            {
                TeacherId = teacherId,
                LessonGroupId = lessonGroupId,
                IsMain = true
            };
            _context.LessonGroupTeachers.Add(newLGT);
            _context.SaveChanges();
            return GetWithIncludes().First(g => g.Id == newLGT.Id);
        }
    }
}
