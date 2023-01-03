using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasarimProjesi.Data;
using TasarimProjesi.Models;

namespace TasarimProjesi.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly INotyfService _notyf;
        public RequestsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, INotyfService notyf)
        {
            _context = context;
            _userManager = userManager;
            _notyf = notyf;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Request.Where(a => a.IsOver == false).ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> MyRequests()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(await _context.Request.Where(a => a.User == user.UserName).ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> OverIndex()
        {
            return View(await _context.Request.Where(a => a.IsOver == true).ToListAsync());
        }

        [Authorize(Roles = "Yönetici, IT")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Request == null)
            {
                return NotFound();
            }

            var request = await _context.Request
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["User"] = user.UserName;
            return View(request);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Yönetici, IT")]
        public async Task<IActionResult> Create(Request request)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            foreach (var file in request.Files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var fileModel = new FileModel
                {
                    CreatedOn = time,
                    FileType = file.ContentType,
                    Extension = extension,
                    Name = fileName
                };
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    fileModel.Data = dataStream.ToArray();
                }
                request.FileList.Add(fileModel);
            }
            request.Date = time;
            request.User = user.UserName;
            _context.Add(request);
            await _context.SaveChangesAsync();
            _notyf.Success("Talep Oluşturuldu");
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Yönetici, IT")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Request == null)
            {
                return NotFound();
            }

            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["User"] = user.UserName;
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Yönetici, IT")]
        public async Task<IActionResult> Edit(int id, Request request)
        {
            string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            foreach (var file in request.Files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var fileModel = new FileModel
                {
                    CreatedOn = time,
                    FileType = file.ContentType,
                    Extension = extension,
                    Name = fileName
                };
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    fileModel.Data = dataStream.ToArray();
                }
                request.FileList.Add(fileModel);
            }
            request.IsOver = true;
            _context.Update(request);
            _notyf.Success("İşlem Başarılı");
            await _context.SaveChangesAsync();
            return View(request);
        }


        [Authorize(Roles = "Yönetici, IT")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Request == null)
            {
                return NotFound();
            }

            var request = await _context.Request
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Yönetici, IT")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Request == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Request'  is null.");
            }
            var request = await _context.Request.FindAsync(id);
            if (request != null)
            {
                _context.Request.Remove(request);
            }

            await _context.SaveChangesAsync();
            _notyf.Success("Talep Silindi");
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.RequestId == id);
        }
    }
}
