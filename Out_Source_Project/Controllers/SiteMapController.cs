using Microsoft.AspNetCore.Mvc;
using Out_Source_Project.Models;
using System.Text;

namespace Out_Source_Project.Controllers
{
	public class SiteMapController : Controller
	{
		private readonly OutSourceContext _context;
		public SiteMapController( OutSourceContext context)
		{
			_context = context;
		}
		public string GetHost()
		{
			return $"{(Request.IsHttps ? "https" : "http")}://{Request.Host}";
		}
		string BaseUrl = "https://dienlanhsaovang.com";
		[Route("sitemap.xml")]
		public IActionResult Index()
		{
			string baseUrl = GetHost();
			List<string> ls = new List<string>();
			ls.Add(baseUrl + "/Sitemap-categories.xml");
			ls.Add(baseUrl + "/new-sitemap.xml");
			var stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			stringBuilder.AppendLine("<sitemapindex xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
			foreach(var item in ls)
			{
				string link = "<loc>"+item+ "</loc>";
				stringBuilder.AppendLine("<sitemap>");
				stringBuilder.AppendLine(link);
				stringBuilder.AppendLine("<lastmod>" + DateTime.Now.ToString("MMMM-dd-yyyy HH:mm:ss tt") + "</lastmod>");
				stringBuilder.AppendLine("</sitemap>");
			}
			stringBuilder.AppendLine("</sitemapindex>");
			return Content(stringBuilder.ToString(), "text/xml", Encoding.UTF8);
		}

		//[Route("sitemap-categories.xml")]
		//public IActionResult SiteMapDanhMuc()
		//{
		//	var lsDanhmuc = _context.Categories.Where(x => x.Published == true).ToList();
		//	var sitemapBuilder = new SitemapBuilder();
		//	sitemapBuilder.AddUrl(BaseUrl, modifed: DateTime.UtcNow, changeFrequency: ChangeFrequency.Weekly, priority: 1.0);
		//	foreach(var item in lsDanhmuc)
		//	{
		//		sitemapBuilder.AddUrl(GetHost() + "/" + item.Alias, modifed: DateTime.UtcNow, changeFrequency: ChangeFrequency.Weekly, priority: 0.9);
		//	}
		//}
	}
}
