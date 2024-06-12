using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTO
{
    public class LessonDTO: ILessonDTO
    {
        public int Id { get; set; }
        public int LessonNumber { get; set; }
        public int Weekday { get; set; }
        public int WeekNumber { get; set; }
        public bool isDistantce { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public Subject Subject { get; set; }
        public ClassroomType? Audience { get; set; }
        public Group Group { get; set; }
    }
}
