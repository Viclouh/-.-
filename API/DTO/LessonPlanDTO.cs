using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTO
{
    public class LessonPlanDTO
    {
        public int Id { get; set; }
        public int LessonNumber { get; set; }
        public Subject Subject { get; set; }
        public AudienceDTO? Audience { get; set; }
        public Group Group { get; set; }
        public bool isDistantсe { get; set; }
        public int Weekday { get; set; }
        public int WeekNumber { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}
