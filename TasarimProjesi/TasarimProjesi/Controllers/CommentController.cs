using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasarimProjesi.Data;
using TasarimProjesi.Models;

namespace TasarimProjesi.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly INotyfService _notyf;

        public CommentController(ApplicationDbContext context, UserManager<IdentityUser> userManager, INotyfService notyf)
        {
            _context = context;
            _userManager = userManager;
            _notyf = notyf;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create(IFormCollection form)
        {
            var comment = form["CommentDetail"].ToString();
            var purchasingItemId = int.Parse(form["PurchasingItemId"]);
            string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            Comment c = new Comment()
            {
                PurchasingItemId = purchasingItemId,
                CommentDetail = comment,
                Time = time
            };
            var user = await _userManager.GetUserAsync(HttpContext.User);
            c.User = user.UserName;

            _context.Comment.Add(c);
            _context.SaveChanges();
            _notyf.Success("Yorum Eklendi");
            return RedirectToAction("Index", "PurchasingItem");
        }
    }
}