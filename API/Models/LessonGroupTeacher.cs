namespace API.Models
{
    public class LessonGroupTeacher
    {
        public int Id { get; set; }
        public int LessonGroupId { get; set; }
        public int TeacherId { get; set; }
        public int Subgroup { get; set; }
        public bool IsMain { get; set; }

        public LessonGroup LessonGroup { get; set; }
        public Teacher Teacher { get; set; }
    }
}
