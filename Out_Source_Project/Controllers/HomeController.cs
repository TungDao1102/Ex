using Microsoft.AspNetCore.Mvc;
using Out_Source_Project.Models;
using System.Diagnostics;

namespace Out_Source_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
		public IActionResult Blog()
        {
            return View();
        }
        public IActionResult Services()
        {
			return View();
		}
		public IActionResult DuAn()
		{
			return View();
		}
        public IActionResult Teams()
        {
            return View();
        }
		public IActionResult Fag()
        {
            return View();
        }
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}