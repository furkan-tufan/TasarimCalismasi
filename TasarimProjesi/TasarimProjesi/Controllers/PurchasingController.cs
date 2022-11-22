using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TasarimProjesi.Data;
using TasarimProjesi.Models;

namespace TasarimProjesi.Controllers
{
    public class PurchasingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PurchasingController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            Purchasing purchasing = new Purchasing();
            purchasing.Items.Add(new PurchasingItem() { PurchasingItemId = 1 });
            return View(purchasing);
        }
        [HttpPost]
        [Authorize]
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
            return RedirectToAction("Home", "Index");

        }
    }
}