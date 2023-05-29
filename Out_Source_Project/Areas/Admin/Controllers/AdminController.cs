using Microsoft.AspNetCore.Mvc;

namespace Out_Source_Project.Areas.Admin.Controllers
{
	public class AdminController : Controller
	{
		[Area("Admin")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
