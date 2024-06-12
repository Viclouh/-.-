namespace API.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int AcademicYear { get; set; }
        public int Semester { get; set; }
        public int ScheduleStatusId { get; set; }

        public ScheduleStatus ScheduleStatus { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}
