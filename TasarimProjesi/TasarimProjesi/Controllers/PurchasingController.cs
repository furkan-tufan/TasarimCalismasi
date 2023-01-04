using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasarimProjesi.Data;
using TasarimProjesi.Models;

namespace TasarimProjesi.Controllers
{
    public class PurchasingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INotyfService _notyf;

        public PurchasingController(ApplicationDbContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }
        [HttpGet]
        [Authorize(Roles = "Yönetici, Satın Alma")]
        public IActionResult Create()
        {
            Purchasing purchasing = new Purchasing();
            purchasing.Items.Add(new PurchasingItem() { PurchasingItemId = 1 });
            return View(purchasing);
        }
        [HttpPost]
        [Authorize(Roles = "Yönetici, Satın Alma")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Purchasing purchasing)
        {
            foreach (PurchasingItem item in purchasing.Items.ToList())
            {
                if (item.Price == 0)
                    purchasing.Items.Remove(item);
            }
            _context.Add(purchasing);
            await _context.SaveChangesAsync();
            _notyf.Success("İşlem Başarılı");
            return RedirectToAction("Index", "PurchasingItem");
        }
	}
}