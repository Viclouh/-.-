namespace API.Models
{
    public class Teacher_Discipline
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int SubjectId { get; set; }
        public Discipline Subject { get; set; }
    }
}
