namespace API.Models
{
    public class Change
    {
        public int Id { get; set; }
        public int LessonNumber { get; set; }
        public int ClassroomId { get; set; }
        public bool IsRemote { get; set; }
        public DateTime Date { get; set; }
        public bool IsCanceled { get; set; }
        public int LessonGroupId { get; set; }

        public Classroom Classroom { get; set; }
        public LessonGroup LessonGroup { get; set; }
    }
}
