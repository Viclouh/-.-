namespace Web.Models
{
    public class GroupTeacher

    {
        public int Id { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
        public Group Group { get; set; }
        public bool IsGeneral { get; set; }
    }
}
