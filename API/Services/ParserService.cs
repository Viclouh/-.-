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
        //public bool Parse(string base64, int year, int semester, int statusId, int department)
        //{
        //    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file.xls");
        //    File.WriteAllBytes(path, Convert.FromBase64String(base64));
        //    var schedule = new Schedule
        //    {
        //        AcademicYear = year,
        //        Semester = semester,
        //        ScheduleStatusId = statusId,
        //    };
        //    _context.Schedules.Add(schedule);
        //    Parsing parsing = new Parsing(path, department, schedule, _context);
        //    return parsing.ParseAllDataAsync();
        //}
    }
}
