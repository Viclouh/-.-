using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public string Code { get; set; }
    }
}
