using API.Database;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels;
using Npgsql;

namespace Web.Controllers
{
	public class MainScheduleController : Controller
	{
		private Context _context = new Context();
		public async Task<IActionResult> Index()
		{

			MainScheduleViewModel model = new MainScheduleViewModel
			{
				Specialities = _context.Speciality.ToList(),
				Groups = _context.Group.Include(x=>x.Speciality).ToList(),
				Teachers = _context.Teacher.ToList(),
			};



			return View(model);
		}
		public async Task<IActionResult> Schedule()
		{
			return PartialView();
		}

		public ActionResult Lesson(int weekday, int lessonNumber, int groupId)
		{
			LessonPlan lp = _context.LessonPlan
				.Where(x=>x.Weekday == weekday && x.LessonNumber == lessonNumber && x.GroupId == groupId).FirstOrDefault();
			if (lp == null)
			{
				return PartialView(new LessonPlan
				{
					Weekday = weekday,
					LessonNumber = lessonNumber,
					GroupId = groupId
				});
			}
			else return PartialView(lp);
		}
	}
}
