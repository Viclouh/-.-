using API.Models;

namespace API.DTO
{
    public interface ILessonDTO
    {
        public int Id { get; set; }
        public int LessonNumber { get; set; }
        public int Weekday { get; set; }
        public int WeekNumber { get; set; }
        public bool isDistantce { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}
