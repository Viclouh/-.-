namespace API.Models
{
    public class ClassroomType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Classroom> Classrooms { get; set; }
    }
}
