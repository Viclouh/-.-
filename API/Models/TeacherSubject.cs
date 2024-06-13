using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class TeacherSubject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }

        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
    }
}
