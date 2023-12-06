using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Course
    {
        public int Id { get; set; }

        public int SpecialityId { get; set; }
        [ForeignKey("SpecialityId")]
        public Speciality Speciality { get; set; }
        public string Name { get; set; }
        public string Shortname { get; set; }
        public List<Group> Groups { get; set; }
    }
}
