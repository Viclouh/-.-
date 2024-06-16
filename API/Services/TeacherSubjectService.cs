using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class TeacherSubjectService
    {
        private readonly Database.Context _context;

        public TeacherSubjectService(Database.Context context)
        {
            _context = context;
        }

        public List<TeacherSubject> GetWithIncludes()
        {
            return _context.TeacherSubjects.Include(ts => ts.Teacher).Include(ts => ts.Subject).ToList();
        }

        public List<TeacherSubject> Get(int? teacherId, int? subjectId)
        {
            if (teacherId.HasValue && subjectId.HasValue)
            {
                return GetWithIncludes().Where(ts => ts.TeacherId == teacherId && ts.SubjectId == subjectId).ToList();
            }
            if (teacherId.HasValue)
            {
                return GetWithIncludes().Where(ts => ts.TeacherId == teacherId).ToList();
            }
            if (subjectId.HasValue)
            {
                return GetWithIncludes().Where(ts => ts.SubjectId == subjectId).ToList();
            }

            return GetWithIncludes().ToList();
        }
    }
}
