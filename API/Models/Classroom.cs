using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Classroom
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Number { get; set; }
        public int? ClassroomTypeId { get; set; }

        public ClassroomType? ClassroomType { get; set; }
        //public ICollection<Lesson> Lessons { get; set; }
        //public ICollection<Change> Changes { get; set; }
    }
}
