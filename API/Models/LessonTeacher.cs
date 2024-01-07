using excel_parcing.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class LessonTeacher
    {
        public int Id { get; set; }
        public LessonPlan Lesson { get; set; }
        public Teacher Teacher { get; set; }
        public bool IsGeneral { get; set; }
    }
}
