//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Caching.Memory;
//using Out_Source_Project.Enums;
//using Out_Source_Project.Models;

//namespace Out_Source_Project.Components
//{
//    public class ExampleViewComponent : ViewComponent
//    {
//        private readonly OutSourceContext _context;
//        private IMemoryCache _cache;
//        // su dung repository interface tot hon su dung context
//        public ExampleViewComponent(OutSourceContext context, IMemoryCache cache)
//        {
//            _context = context;
//            _cache = cache;
//        }
//        public IViewComponentResult Invoke()
//        {
//            var lst = _cache.GetOrCreate(CacheKeys.Categories, entry =>
//            {
//                entry.SlidingExpiration = TimeSpan.FromHours(1);
//                return _context.Categories.ToList();
//            });
//            return View(lst);
//        }
        
//    }
//}
