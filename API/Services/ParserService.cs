using API.Models;
using System.Reflection.Metadata;

namespace API.Services
{
    public class ParserService
    {
        private Database.Context _context;

        public ParserService(Database.Context context)
        {
            _context = context;
        }
        public bool Parse(string base64, int year, int semester, int statusId, int department)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file.xls");
            File.WriteAllBytes(path, Convert.FromBase64String(base64));
            Schedule schedule;
            if (!_context.Schedules.Any(s => s.AcademicYear == year && s.Semester == semester && s.ScheduleStatusId == statusId))
            {
                schedule = new Schedule
                {
                    AcademicYear = year,
                    Semester = semester,
                    ScheduleStatusId = statusId,
                };
                _context.Schedules.Add(schedule);
                _context.SaveChanges();
            }
            else
            {
                schedule = _context.Schedules.FirstOrDefault(s => s.AcademicYear == year && s.Semester == semester && s.ScheduleStatusId == statusId);
            }
            Parsing parsing = new Parsing(path, department, schedule, _context);
            var result = parsing.ParseAllDataAsync();
            return result;
        }
    }
}
