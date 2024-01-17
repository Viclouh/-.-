using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Web.Models;
using Web.Services.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
	public class MainScheduleController : Controller
	{
		private readonly IScheduleService _service;
		private MainScheduleViewModel _viewModel;

		public MainScheduleController(IScheduleService service)
		{
			_service = service ?? throw new ArgumentNullException(nameof(service));
		}

		public async Task<IActionResult> Index()
		{
			var task = GetModel();
			task.Wait();
			return View(_viewModel);
		}

		[Route("Schedule")]
		public async Task<IActionResult> Schedule(int spec)
		{
			Task task;
			if (spec == 0)
				task = GetModel();
			else
				task = GetModel(spec);
			task.Wait();
			return PartialView(_viewModel);
		}

		public ActionResult Lesson(int weekday, int lessonNumber, int groupId, int weekNumber)
		{
/*			LessonPlan lesson = _viewModel.Lessons.Where(x=>x.Weekday==weekday
			&&x.LessonNumber==lessonNumber&&x.Group.Id==groupId&&x.WeekNumber==weekNumber)
				.FirstOrDefault();

			if (lesson == null)
			{
				return PartialView(new LessonPlan
				{
					Weekday = weekday,
					LessonNumber = lessonNumber,
					GroupId = groupId
				});
			}*/
			return PartialView();
		}

		public async Task GetModel()
		{
			_viewModel = new MainScheduleViewModel
			{
				//TODO ещё заглушка
				Specialities = await _service.GetSpecialities(),
				Groups = await _service.GetGroups(),
				Lessons = await _service.GetLessons(),
				Teachers = new List<Teacher>(),
			};
		}
		public async Task GetModel(int spec)
		{
			var Groups = await _service.GetGroups();
			var Lessons = await _service.GetLessons();

			_viewModel = new MainScheduleViewModel
			{
				Specialities = await _service.GetSpecialities(),
				Groups = Groups.Where(x=>x.Speciality.Id == spec),
				Lessons = Lessons.Where(x=>x.Group.Speciality.Id == spec),
				Teachers = new List<Teacher>(),
			};
		}
		public LessonPlan GetLesson()
		{
			return new LessonPlan();
		}
	}
}
