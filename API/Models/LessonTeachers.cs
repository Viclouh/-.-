using excel_parcing.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class LessonTeachers
    {
        public int Id { get; set; }
        [Column("Scheduled-lesson")]
        public Main_Lesson Lesson { get; set; }
        public Teacher Teacher { get; set; }
        public bool IsGeneral { get; set; }
    }
}
