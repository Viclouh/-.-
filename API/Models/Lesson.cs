using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Lesson
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int LessonNumber { get; set; }
        public int ScheduleId { get; set; }
        public bool IsRemote { get; set; }
        public int DayOfWeek { get; set; }
        public int WeekOrderNumber { get; set; }
        public int? ClassroomId { get; set; }
        public int LessonGroupId { get; set; }

        public Schedule Schedule { get; set; }
        public Classroom? Classroom { get; set; }
        public LessonGroup LessonGroup { get; set; }
    }
}
