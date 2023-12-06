using API.Database;
using API.Models;
namespace Web.ViewModels
{
	public class TreeviewViewModel
	{
		public Speciality Speciality { get; set; }
		public List<CourseGroups> CourseGroups { get; set; }
		public TreeviewViewModel()
		{
			CourseGroups = new List<CourseGroups>();
			Context context= new Context();
			foreach (Course course in context.Courses.Where(x=>x.Speciality == Speciality).ToList())
			{
				CourseGroups.Add(new CourseGroups(course));
			}
		}
	}
	public class CourseGroups
	{
		public Course Course { get; set; }
		public IEnumerable<Group> Groups { get; set; }
		public CourseGroups(Course _course)
		{
			Course = _course;
			Context context = new Context();
			Groups = context.Groups.Where(x=>x.Course == Course).ToList();
		}
	}
}
