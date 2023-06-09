using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Out_Source_Project.Enums;
using Out_Source_Project.Helper;
using Out_Source_Project.Models;
using Out_Source_Project.Models.ViewModel;
using System.Diagnostics;
using X.PagedList;

namespace Out_Source_Project.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly OutSourceContext _context;
		public HomeController(ILogger<HomeController> logger, OutSourceContext context)
		{
			_logger = logger;
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var lstPost = await _context.Posts.AsNoTracking().Select(p => new Post
			{
				Scontents = p.Scontents,
				Title = p.Title,
				CreatedDate = p.CreatedDate,
				Thumb = p.Thumb
			}).OrderByDescending(x => x.CreatedDate).Take(2).ToListAsync();
			return View(lstPost);
		}

		public IActionResult About()
		{
			return View();
		}
		public async Task<IActionResult> Blog(int? page)
		{

			int pageSize = 9;
			int pageNumber = (page == null || page < 0) ? 1 : page.Value;
			var lstPost = await _context.Posts.Select(p => new Post
			{
				Scontents = p.Scontents,
				Title = p.Title,
				CreatedDate = p.CreatedDate,
				Thumb = p.Thumb,
				Alias = p.Alias
			}).OrderByDescending(x => x.CreatedDate).ToListAsync();

			PagedList<Post> lst = new PagedList<Post>(lstPost, pageNumber, pageSize);
			return View(lst);
		}
		[Route("/{Alias}.html", Name = "BlogDetail")]
		public async Task<IActionResult> BlogDetail(string Alias)
		{
			if (string.IsNullOrEmpty(Alias))
			{
				return NotFound();
			}

			//var BlogVM = await _cache.GetOrCreateAsync(CacheKeys.BlogViewModel, entry =>
			//{
			//	entry.SlidingExpiration = TimeSpan.FromMinutes(10);
			//	return _context.Posts
			//		.Select(x => new BlogViewModel
			//		{
			//			Title = x.Title,
			//			Thumb = x.Thumb,
			//			Contents = x.Contents,
			//			Scontents = x.Scontents,
			//			CreateDate = x.CreatedDate,
			//			Author = x.Author,
			//			Alias = x.Alias,
			//			Cover = x.Cat.Cover,
			//			Icon = x.Cat.Icon,
			//			Thumbnail = x.Cat.Thumb,
			//			CatName = x.Cat.CatName
			//		})
			//		.SingleOrDefaultAsync(x => x.Alias == Alias);
			//});
			
			var BlogVM = await _context.Posts
					.Select(x => new BlogViewModel
					{
						Title = x.Title,
						Thumb = x.Thumb,
						Contents = Ultilities.StripHTML(x.Contents),
						Scontents = x.Scontents,
						CreateDate = x.CreatedDate,
						Author = x.Author,
						Alias = x.Alias,
						Cover = x.Cat.Cover,
						Icon = x.Cat.Icon,
						Thumbnail = x.Cat.Thumb,
						CatName = x.Cat.CatName
					})
					.SingleOrDefaultAsync(x => x.Alias == Alias);
			if (BlogVM == null)
			{
				return NotFound();
			}
			return View(BlogVM);
		}



		public IActionResult Services()
		{
			return View();
		}
		public IActionResult ServiceDetail() { return View(); }
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
		public IActionResult Contact()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Contact(Message msg)
		{
			if (ModelState.IsValid)
			{
				 _context.Messages.Add(msg);
				await _context.SaveChangesAsync();
				TempData["SuccessMessage"] = "Thông tin của quý khách đã được ghi lại, chúng tôi sẽ liên hệ với quý khách trong thời gian sớm nhất. Xin cảm ơn!";
				return RedirectToAction(nameof(Index));
			}
			return View(msg);
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


	}
}