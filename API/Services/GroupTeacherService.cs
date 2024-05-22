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

        public IEnumerable<GroupTeacher> Get(int? groupId, int? teacherId)
        {
            if (groupId != null && teacherId != null)
            {
                return _context.GroupTeacher
                    .Include(gt => gt.Group)
                    .ThenInclude(g => g.Speciality)
                    .Include(gt => gt.Teacher)
                    .Include(gt => gt.Subject)
                    .Where(gt => gt.Group.Id == groupId && gt.Teacher.Id == teacherId);
            }
            if (groupId != null)
            {
                return _context.GroupTeacher
                    .Include(gt => gt.Group)
                    .ThenInclude(g => g.Speciality)
                    .Include(gt => gt.Teacher)
                    .Include(gt => gt.Subject)
                    .Where(gt => gt.Group.Id == groupId);
            }
            if (teacherId != null)
            {
                return _context.GroupTeacher
                    .Include(gt => gt.Group)
                    .ThenInclude(g => g.Speciality)
                    .Include(gt => gt.Teacher)
                    .Include(gt => gt.Subject)
                    .Where(gt => gt.Teacher.Id == teacherId);
            }

            return _context.GroupTeacher
                .Include(gt => gt.Group)
                .ThenInclude(g => g.Speciality)
                .Include(gt => gt.Teacher)
                .Include(gt => gt.Subject);
        }
        public bool Delete(int id)
        {
            var item = _context.GroupTeacher.Where(gt => gt.Id == id).FirstOrDefault();
            if (item == null)
            {
                return false;
            }
            _context.GroupTeacher.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public GroupTeacher Post(GroupTeacherDTO groupTeacher)
        {
            GroupTeacher newGroupTeacher = new GroupTeacher
            {
                GroupId = groupTeacher.Group.Id,
                SubjectId = groupTeacher.Subject.Id,
                TeacherId = groupTeacher.Teacher.Id,
                IsGeneral = false
            };
            _context.GroupTeacher.Add(newGroupTeacher);
            _context.SaveChanges();

            return _context.GroupTeacher
                .Where(gt => gt.Teacher.Id == groupTeacher.Teacher.Id && gt.Group.Id == groupTeacher.Group.Id && gt.Subject.Id == groupTeacher.Subject.Id)
                .Include(gt => gt.Teacher)
                .Include(gt => gt.Subject)
                .Include(gt => gt.Group)
                .FirstOrDefault();
        }
    }
}
