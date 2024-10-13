using E_Commerce.Data;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public IActionResult Index()
        {
            var Categories = _context.categories.ToList();
            return View(Categories);
        }
    }
}
