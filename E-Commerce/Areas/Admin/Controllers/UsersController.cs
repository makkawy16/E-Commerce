using E_Commerce.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string userid = claim.Value;

            return View(_context.ApplicationUsers.Where(x=>x.Id !=userid).ToList());
        }
    }
}
