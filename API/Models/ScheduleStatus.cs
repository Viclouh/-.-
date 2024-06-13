using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class ScheduleStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }
}
