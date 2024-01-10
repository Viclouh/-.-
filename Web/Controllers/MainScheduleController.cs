using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Web.Models;
using Web.Services.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
	public class MainScheduleController : Controller
	{
		private readonly ISpecialityService _service;

		public MainScheduleController(ISpecialityService service)
		{
			_service = service ?? throw new ArgumentNullException(nameof(service));
		}

		public async Task<IActionResult> Index()
		{
			MainScheduleViewModel model = new MainScheduleViewModel
			{
				//TODO ещё заглушка
				Specialities = await _service.Find(),
				Groups = new List<Group>(),
				Teachers = new List<Teacher>(),
			};

			return View(model);
		}
		public async Task<IActionResult> Schedule()
		{
			return PartialView();
		}

		public ActionResult Lesson(int weekday, int lessonNumber, int groupId)
		{
			//TODO: вот это заглушка
			LessonPlan lp = new List<LessonPlan>()
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
