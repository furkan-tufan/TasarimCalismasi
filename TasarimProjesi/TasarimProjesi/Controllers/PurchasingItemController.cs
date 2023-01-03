using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasarimProjesi.Data;
using TasarimProjesi.Models;

namespace TasarimProjesi.Controllers
{
    public class PurchasingItemController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly INotyfService _notyf;

		public PurchasingItemController(ApplicationDbContext context, INotyfService notyf, UserManager<IdentityUser> userManager)
        {
            _context = context;
			_notyf = notyf;
			_userManager = userManager;
		}

        [Authorize(Roles = "Yönetici, Satın Alma")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PurchasingItem.Include(p => p.Purchasing);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Yönetici, Satın Alma")]
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

        [Authorize(Roles = "Yönetici, Satın Alma")]
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

        [Authorize(Roles = "Yönetici, Satın Alma")]
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

        [Authorize(Roles = "Yönetici, Muhasebe")]
        [HttpGet]
		public async Task<IActionResult> AccountingIndex()
		{
			var data = _context.PurchasingItem.Include(p => p.Purchasing).Where(a => a.Uploaded==false);
			return View(await data.ToListAsync());
		}

		[HttpGet]
        [Authorize(Roles = "Yönetici, Muhasebe")]
        public async Task<IActionResult> Accounting(int? id)
		{
            var purchasingItem = await _context.PurchasingItem
                .Include(p => p.Purchasing)
                .FirstOrDefaultAsync(m => m.PurchasingItemId == id);
            ViewBag.Id = id.Value;
            return View(purchasingItem);
		}

        [HttpPost]
        [Authorize(Roles = "Yönetici, Muhasebe")]
        public async Task<IActionResult> Invoice(List<IFormFile> invoices)
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);
			int id = Convert.ToInt32(Request.Form["id"]);
			var item = await _context.PurchasingItem.FirstOrDefaultAsync(m => m.PurchasingItemId == id);
			foreach (var file in invoices)
			{
				if (file != null)
				{
					var fileName = Path.GetFileNameWithoutExtension(file.FileName);
					var extension = Path.GetExtension(file.FileName);
					var fileModel = new FileModel
					{
						FileType = file.ContentType,
						Extension = extension,
						Name = fileName,
						UploadedBy = user.UserName
					};
					using (var dataStream = new MemoryStream())
					{
						await file.CopyToAsync(dataStream);
						fileModel.Data = dataStream.ToArray();
					}
					item.FileList.Add(fileModel);
				}
			}
            item.Uploaded = true;
			_context.Update(item);
			await _context.SaveChangesAsync();
			_notyf.Success("Fatura Yüklendi!");
			return RedirectToAction("AccountingIndex", "PurchasingItem");
		}
	}
}