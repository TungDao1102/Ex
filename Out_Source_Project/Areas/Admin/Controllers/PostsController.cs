using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Out_Source_Project.Helper;
using Out_Source_Project.Models;
using Out_Source_Project.Models.Authentication;
using X.PagedList;

namespace Out_Source_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly OutSourceContext _context;

        public PostsController(OutSourceContext context)
        {
            _context = context;
        }

        // GET: Admin/Posts
        [Authentication]
        public async Task<IActionResult> Index(int? page, int catID =0)
        {

            int pageSize = 10;
            int pageNumber = (page == null || page < 0) ? 1 : page.Value;
            //  var lstPost = await _context.Posts.Include(p => p.Account).Include(p => p.Cat).ToListAsync();
            //List<Post> lstPost = new List<Post>();
            //if (catID !=0)
            //{
            //     lstPost = await _context.Posts.Include(p => p.Cat).Where(x => x.CatId == catID).Select(p => new Post
            //    {
            //        PostId = p.PostId,
            //        Title = p.Title,
            //        Cat = p.Cat,
            //        Published = p.Published,
            //        Alias = p.Alias
            //    }).OrderByDescending(x => x.PostId).ToListAsync();
            //}
            //else
            //{
            //     lstPost = await _context.Posts.Include(p => p.Account).Include(p => p.Cat).Select(p => new Post
            //    {
            //        PostId = p.PostId,
            //        Title = p.Title,
            //        Cat = p.Cat,
            //        Published = p.Published,
            //        Alias = p.Alias
            //    }).OrderByDescending(x => x.PostId).ToListAsync();
            //}
            var lstPost = await _context.Posts.Include(p => p.Cat).Select(p => new Post
            {
                PostId = p.PostId,
                Title = p.Title,
                Cat = p.Cat,
                Published = p.Published,
                Alias = p.Alias
            }).OrderByDescending(x => x.PostId).ToListAsync();
            PagedList <Post> lst = new PagedList<Post>(lstPost, pageNumber, pageSize);
			ViewBag.DanhMuc = new SelectList(_context.Categories, "CatId", "CatName");
			return View(lst);

        }


        // GET: Admin/Posts/Create
        [Authentication]
        public IActionResult Create()
        {
          //  ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
            ViewBag.DanhMuc = new SelectList(_context.Categories, "CatId", "CatName");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaDesc,MetaKey,MetaTitle")] Post post, IFormFile? fThumb)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Login", "Admin", new { Area = "Admin" });
            //}
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            //if(taikhoanID == null)
            //{
            //    return RedirectToAction("Login", "Admin", new { Area = "Admin" });
            //}
            var account = _context.Accounts.AsNoTracking().SingleOrDefault(x => x.AccountId == int.Parse(taikhoanID));
            //if(account == null)
            //{
            //    return NotFound();
            //}
            if (ModelState.IsValid)
            {
                post.AccountId = account.AccountId;
                post.Author = account.FullName;
                if(post.CatId ==null)
                {
                    post.CatId = 1;
                }
                post.CreatedDate = DateTime.Now;
                
                post.Alias = Ultilities.SEOUrl(post.Title);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string newName = Ultilities.SEOUrl(post.Title)  + extension;
                    post.Thumb = await Ultilities.UploadFile(fThumb, @"posts\", newName.ToLower());
                }
                //if (post.IsNewfeed == null)
                //{
                //    post.IsNewfeed = false;
                //}
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DanhMuc = new SelectList(_context.Categories, "CatId", "CatName", post.CatId);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        [Authentication]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }
            ViewBag.DanhMuc = new SelectList(_context.Categories, "CatId", "CatName");
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaDesc,MetaKey,MetaTitle")] Post post, IFormFile? fThumb)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Login", "Admin", new { Area = "Admin" });
            //}
            //var taikhoanID = HttpContext.Session.GetString("AccountId");
            //if (taikhoanID == null)
            //{
            //    return RedirectToAction("Login", "Admin", new { Area = "Admin" });
            //}
            //var account = _context.Accounts.AsNoTracking().FirstOrDefault(x => x.AccountId == int.Parse(taikhoanID));
            //if (account == null)
            //{
            //    return NotFound();
            //}
            //if(account.RoleId != ) { 
            //    if(post.AccountId != account.AccountId)
            //    {
            //        return RedirectToAction(nameof(Index));
            //    }
            //}
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    post.Alias = Ultilities.SEOUrl(post.Title);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string newName = Ultilities.SEOUrl(post.Title) + "_preview" + extension;
                        post.Thumb = await Ultilities.UploadFile(fThumb, @"posts\", newName.ToLower());
                    }
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", post.AccountId);
            //ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", post.CatId);
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        [Authentication]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            //var post = await _context.Posts
            //    .Include(p => p.Account)
            //    .Include(p => p.Cat)
            //    .FirstOrDefaultAsync(m => m.PostId == id);
            var post = await _context.Posts
                .Where(m => m.PostId == id)
                .Select(p => new Post{
                   PostId= p.PostId,
                   Title= p.Title,
                   Published=  p.Published,
                   Scontents=  p.Scontents,
                   Thumb =  p.Thumb
          //          AccountName = p.Account.FullName,
          //          CatName = p.Cat.CatName
                })
                .FirstOrDefaultAsync();

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'OutSourceContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return (_context.Posts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }

        //public IActionResult PostFilter(int catID = 0)
        //{
        //    var url = $"/Admin/Posts/Index?catID={catID}";
        //    if (catID == 0)
        //    {
        //        url = $"/Admin/Posts/Index";
        //    }
        //    else
        //    {
        //        url = $"/Admin/Posts/Index?catID={catID}";
        //    }
        //    return Json(new { status = "success", redirectUrl = url });
        //}

        //public IActionResult FindPost(string keyword)
        //{
        //    if (keyword != null && keyword.Trim().Length > 3)
        //    {
        //        var posts = _context.Posts.Include(p => p.Cat).AsNoTracking().Where(p => p.Title.Contains(keyword)
        //        || p.Contents.Contains(keyword)).OrderByDescending(x => x.CreatedDate).ToList();
        //        return PartialView("_FindPost", posts);
        //    }
        //    else
        //    {
        //        return PartialView("_FindPost", null);
        //    }
        //}



    }
}
