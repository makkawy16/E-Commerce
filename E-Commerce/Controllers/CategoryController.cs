using E_Commerce.Data;
using E_Commerce.Models;
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
        [HttpGet]
        //this action is in form of get because it generates new page
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        //we need to get info from user
        //Controller is the link between model and the view
        public IActionResult Create(Category category)
        {
            _context.categories.Add(category);
            _context.SaveChanges(); 

            return RedirectToAction("Index");
        }
    }
}
