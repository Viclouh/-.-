using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace API.Models
{
    public class LessonGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public string ScheduleType { get; set; }

        public Subject Subject { get; set; }
        public Group Group { get; set; }
        public ICollection<LessonGroupTeacher> LessonGroupTeachers { get; set; }
    }
}
