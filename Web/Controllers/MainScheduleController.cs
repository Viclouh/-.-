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


			return View(context.Specialities
				.Include(x=>x.Courses)
				.ThenInclude(x=>x.Groups).ToList()
				);
		}
		public async Task<IActionResult> Schedule()
		{
			Context context = new Context();
			MainScheduleViewModel model = new MainScheduleViewModel
			{
				Specialities = context.Specialities.ToList(),
				Groups = context.Groups.ToList(),
				Teachers = context.Teachers.ToList(),
			};
			return PartialView();
		}
	}
}
