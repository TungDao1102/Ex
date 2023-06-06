using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Out_Source_Project.Helper;
using Out_Source_Project.Models;
using X.PagedList;

namespace Out_Source_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly OutSourceContext _context;

        public CategoriesController(OutSourceContext context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index(int? page)
        {
			int pageSize = 10;
			int pageNumber = (page == null || page < 0) ? 1 : page.Value;
            var lstCategory = await _context.Categories.AsNoTracking().OrderBy(c => c.CatId).ToListAsync();
            PagedList<Category> lst = new PagedList<Category>(lstCategory, pageNumber, pageSize);
			ViewBag.CurrentPage = pageNumber;
			return View(lst);
        }


        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
			ViewBag.DanhMucGoc = new SelectList(_context.Categories.Where(x => x.Levels==1), "CatId", "CatName");
			return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatId,CatName,Title,Alias,MetaDesc,MetaKey,Thumb,Published,Ordering,Parents,Levels,Icon,Cover,Description")] Category category, IFormFile? fThumb, IFormFile? fCover, IFormFile? fIcon)
        {
            ViewBag.DanhMucGoc = new SelectList(_context.Categories.Where(x => x.Levels == 1), "CatId", "CatName");
            if (ModelState.IsValid)
            {
                category.Alias = Ultilities.SEOUrl(category.CatName);
				if(category.Parents == null)
                {
                    category.Levels = 1;
                }
                else
                {
					category.Levels = category.Parents == 0 ? 1 : 2;
				}
				if(fThumb !=null && fThumb.Length > 0)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
					string newName = Ultilities.SEOUrl(category.CatName) +"_preview"+ extension;
					category.Thumb = await Ultilities.UploadFile(fThumb, @"categories\", newName.ToLower());
				}
				if (fCover != null && fCover.Length > 0)
				{
					string extension = Path.GetExtension(fCover.FileName);
					string newName = "cover_"+ Ultilities.SEOUrl(category.CatName) + extension;
					category.Cover = await Ultilities.UploadFile(fCover, @"cover\", newName.ToLower());
				}
				if (fIcon != null && fIcon.Length > 0)
				{
					string extension = Path.GetExtension(fIcon.FileName);
					string newName = "icon_"+ Ultilities.SEOUrl(category.CatName) + extension;
					category.Icon = await Ultilities.UploadFile(fIcon, @"icon\", newName.ToLower());
				}  
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            ViewBag.DanhMucGoc = new SelectList(_context.Categories.Where(x => x.Levels == 1), "CatId", "CatName");
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatId,CatName,Title,Alias,MetaDesc,MetaKey,Thumb,Published,Ordering,Parents,Levels,Icon,Cover,Description")] Category category, IFormFile? fThumb , IFormFile? fCover, IFormFile? fIcon)
        {
            if (id != category.CatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    category.Alias = Ultilities.SEOUrl(category.CatName);
					if (category.Parents == null)
					{
						category.Levels = 1;
					}
					else
					{
						category.Levels = category.Parents == 0 ? 1 : 2;
					}
					if (fThumb != null)
					{
						string extension = Path.GetExtension(fThumb.FileName);
						string newName = Ultilities.SEOUrl(category.CatName) + "_preview" + extension;
						category.Thumb = await Ultilities.UploadFile(fThumb, @"categories\", newName.ToLower());
					}
                    if (fCover != null)
					{
						string extension = Path.GetExtension(fCover.FileName);
						string newName = "cover_" + Ultilities.SEOUrl(category.CatName) + extension;
						category.Cover = await Ultilities.UploadFile(fCover, @"cover\", newName.ToLower());
                    }

                    if (fIcon != null)
                    {
						string extension = Path.GetExtension(fIcon.FileName);
						string newName = "icon_" + Ultilities.SEOUrl(category.CatName) + extension;
						category.Icon = await Ultilities.UploadFile(fIcon, @"icon\", newName.ToLower());
					}
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CatId))
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
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'OutSourceContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.CatId == id)).GetValueOrDefault();
        }
    }
}
