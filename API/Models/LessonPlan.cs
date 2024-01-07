using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("lesson_plan")]
    public class LessonPlan
    {
        public int Id { get; set; }
        public int LessonNumber { get; set; }
        public Subject Subject { get; set; }
        public Audience? Audience { get; set; }
        public Group Group { get; set; }
        public bool isDistantсe { get; set; }
        public int Weekday { get; set; }
        public int? WeekNumber { get; set; }
        [NotMapped]
        public List<Teacher> Teachers { get; set; }
    }
}
