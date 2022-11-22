using TasarimProjesi.Data;
using TasarimProjesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace TasarimProjesi.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CommentController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize]
        public ActionResult Details(int? id)
        {
            Comment commentt = _context.Comment.Find(id);
            return View(commentt);
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

            return RedirectToAction("Index", "PurchasingItem");
        }

    }
}
