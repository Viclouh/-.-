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
			List<TreeviewViewModel> treeviews = new List<TreeviewViewModel>();
			foreach (Speciality speciality in context.Specialities.ToList())
			{
				treeviews.Add(new TreeviewViewModel
				{
					Speciality = speciality,
				});
			}
			return View(context.Specialities.ToList());
		}
	}
}
