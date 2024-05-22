using API.Models;

namespace API.DTO
{
    public class GroupTeacherDTO
    {
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
        public Group Group { get; set; }
    }
}
