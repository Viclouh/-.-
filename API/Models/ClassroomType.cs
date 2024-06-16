using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class ClassroomType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        //public ICollection<Classroom> Classrooms { get; set; }
    }
}
