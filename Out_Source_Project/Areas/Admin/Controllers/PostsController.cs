using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Out_Source_Project.Helper;
using Out_Source_Project.Models;
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
        public async Task<IActionResult> Index(int? page)
        {

            int pageSize = 10;
            int pageNumber = (page == null || page < 0) ? 1 : page.Value;
            //  var lstPost = await _context.Posts.Include(p => p.Account).Include(p => p.Cat).ToListAsync();
            var lstPost = await _context.Posts.Include(p => p.Account).Include(p => p.Cat).Select(p => new Post
        {
            PostId = p.PostId,
            Title = p.Title,
            Cat = p.Cat,
            Published = p.Published,
            Alias = p.Alias
        }).OrderByDescending(x => x.PostId).ToListAsync();
            PagedList<Post> lst = new PagedList<Post>(lstPost, pageNumber, pageSize);
            return View(lst);

        }


        // GET: Admin/Posts/Create
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
            if (ModelState.IsValid)
            {
                post.Alias = Ultilities.SEOUrl(post.Title);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string newName = Ultilities.SEOUrl(post.Title) + "_preview" + extension;
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
            ViewBag.DanhMuc = new SelectList(_context.Categories, "CatId", "CatName");
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
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
                        string newName = Ultilities.SEOUrl(post.MetaTitle) + "_preview" + extension;
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
    }
}
