namespace API.Models
{
    public class ScheduleStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }
}
