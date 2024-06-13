using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Subject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public ICollection<TeacherSubject> TeacherSubjects { get; set; }
        public ICollection<LessonGroup> LessonGroups { get; set; }
    }
}
