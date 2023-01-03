using DiemPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasarimProjesi.Data;

namespace TasarimProjesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(UserManager<IdentityUser> userManager, ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        [Authorize]
        public IActionResult HomePage()
        {
            return View();
        }

        [Authorize(Roles = "İnsan Kaynakları, Yönetici")]
        public async Task<IActionResult> UserList()
        {
            var users = await _userManager.Users.ToListAsync();
            var model = new List<UserModel>();
            foreach (IdentityUser user in users)
            {
                var thisModel = new UserModel();
                thisModel.UserId = user.Id;
                thisModel.UserName = user.UserName;
                model.Add(thisModel);
            }
            return View(model);
        }
    }
}