using API.Models;

namespace API.Generator.Entities
{
    public class FitnessSchedule
    {
        public List<Lesson> LessonPlans { get; set; } = new List<Lesson>();
        public int Fitness { get; set; }
    }
}
