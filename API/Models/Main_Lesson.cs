namespace API.Models
{
	public class Main_Lesson
	{
		public int Id { get; set; }
		public int LessonNumber { get; set; }
		public int SubjectId { get; set; }
		public Discipline Discipline { get; set; }
		public int CabinetId { get; set; }
		public Cabinet Cabinet { get; set; }
		public int GroupId { get; set; }
		public Group Group { get; set; }
		public bool isDistantсe { get; set; }
		public int Weekday { get; set; }
		public int? WeekNumber { get; set; }
	}
}
