using API.Database;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels;

namespace Web.Controllers
{
	public class MainScheduleController : Controller
	{
		public async Task<IActionResult> Index()
		{
			Context context = new Context();

			MainScheduleViewModel model = new MainScheduleViewModel
			{
				Specialities = context.Specialities.ToList(),
				Groups = context.Groups.Include(x=>x.Course).ToList(),
				Teachers = context.Teachers.ToList(),
			};

/*			return View(context.Specialities
				.Include(x=>x.Courses)
				.ThenInclude(x=>x.Groups).ToList()
				);*/

			return View(model);
		}
		public async Task<IActionResult> Schedule()
		{
			Context context = new Context();
			return PartialView();
		}
	}
}
