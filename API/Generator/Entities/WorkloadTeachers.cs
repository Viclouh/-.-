using API.Models;

namespace API.Generator.Entities
{
    public class WorkloadTeachers
    {
        public Subject Subject { get; set; }
        public List<Teacher> Teachers { get; set; }
        public double HoursPerWeek { get; set; }
    }
}
