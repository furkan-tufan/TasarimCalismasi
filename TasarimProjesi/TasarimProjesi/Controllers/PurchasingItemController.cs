using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TasarimProjesi.Data;
using TasarimProjesi.Models;

namespace TasarimProjesi.Controllers
{
    public class PurchasingItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchasingItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchasingItem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PurchasingItem.Include(p => p.Purchasing);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PurchasingItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var purchasingItem = await _context.PurchasingItem
                .Include(p => p.Purchasing)
                .FirstOrDefaultAsync(m => m.PurchasingItemId == id);
            ViewBag.PurchasingItemId = id.Value;

            var comments = _context.Comment.Where(d => d.PurchasingItemId.Equals(id.Value)).ToList();
            ViewBag.Comments = comments;

            return View(purchasingItem);
        }

        // GET: PurchasingItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PurchasingItem == null)
            {
                return NotFound();
            }

            var purchasingItem = await _context.PurchasingItem
                .Include(p => p.Purchasing)
                .FirstOrDefaultAsync(m => m.PurchasingItemId == id);
            if (purchasingItem == null)
            {
                return NotFound();
            }

            return View(purchasingItem);
        }

        // POST: PurchasingItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PurchasingItem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PurchasingItem'  is null.");
            }
            var purchasingItem = await _context.PurchasingItem.FindAsync(id);
            if (purchasingItem != null)
            {
                _context.PurchasingItem.Remove(purchasingItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchasingItemExists(int id)
        {
          return _context.PurchasingItem.Any(e => e.PurchasingItemId == id);
        }
    }
}
