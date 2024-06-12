namespace API.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int Department { get; set; }
        public string GroupCode { get; set; }

        public ICollection<LessonGroup> LessonGroups { get; set; }
    }
}
