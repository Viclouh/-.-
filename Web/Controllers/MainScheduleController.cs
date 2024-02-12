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
		private LessonViewModel _lessonViewModel;
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
			//test
			Task task;
			if (spec == 0)
				task = GetModel();
			else
				task = GetModel(spec);
			task.Wait();
			return PartialView(_viewModel);
		}

		public async Task<IActionResult> Lesson(int weekday, int lessonNumber, int groupId, int weekNumber)
		{
			var task = GetLesson(weekday, lessonNumber, groupId, weekNumber);
			task.Wait();
			return PartialView(_lessonViewModel);
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
		public async Task GetLesson(int weekday, int lessonNumber, int groupId, int weekNumber)
		{
			LessonPlan lesson = await _service.GetLesson(weekday, lessonNumber, groupId, weekNumber);

			_lessonViewModel = new LessonViewModel
			{
				Lesson = lesson,
				Teachers = await _service.GetTeachers(),
				Exist = lesson.Id == 0 ? false : true,
			};
		}
	}
}
