using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Department { get; set; }
        public string GroupCode { get; set; }

        //public ICollection<LessonGroup> LessonGroups { get; set; }
    }
}
