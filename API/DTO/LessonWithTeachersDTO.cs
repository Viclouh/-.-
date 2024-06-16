namespace API.DTO
{
    public class LessonWithTeachersDTO
    {
        public LessonDTO Lesson { get; set; }
        public List<TeacherDTO> Teachers { get; set; }
    }

    public class TeacherDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public bool IsMain { get; set; }
    }
}
