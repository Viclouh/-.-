using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTO
{
    public class LessonPlanForMobileDTO : ILessonPlanDTO
    {
        public int Id { get; set; }
        public int LessonNumber { get; set; }
        public string SubjectName { get; set; }
        public string ShortSubjectName { get; set; }
        public string Audiebce { get; set; }
        public string Group { get; set; }
        public bool isDistantce { get; set; }
        public int Weekday { get; set; }
        public int WeekNumber { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}
