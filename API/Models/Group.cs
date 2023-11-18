namespace API.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string Code { get; set; }
    }
}
