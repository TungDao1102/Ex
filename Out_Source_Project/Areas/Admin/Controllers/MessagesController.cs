using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Out_Source_Project.Models;
using Out_Source_Project.Models.Authentication;

namespace Out_Source_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessagesController : Controller
    {
        private readonly OutSourceContext _context;

        public MessagesController(OutSourceContext context)
        {
            _context = context;
        }

        // GET: Admin/Messages
        [Authentication]
        public async Task<IActionResult> Index()
        {
              return _context.Messages != null ? 
                          View(await _context.Messages.ToListAsync()) :
                          Problem("Entity set 'OutSourceContext.Messages'  is null.");
        }

        // GET: Admin/Messages/Details/5
        [Authentication]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.MsgId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }
        // GET: Admin/Messages/Delete/5
        [Authentication]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.MsgId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Admin/Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Messages == null)
            {
                return Problem("Entity set 'OutSourceContext.Messages'  is null.");
            }
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
          return (_context.Messages?.Any(e => e.MsgId == id)).GetValueOrDefault();
        }
    }
}
