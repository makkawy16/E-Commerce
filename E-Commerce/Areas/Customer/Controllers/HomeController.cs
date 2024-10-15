using E_Commerce.Data;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;

        }
        //Index: home page or default view
        public IActionResult Index()
        {
            var products = _context.products.ToList();
            return View(products);


  
        }

        public IActionResult Details()
        {
            var products = _context.products.ToList();
            return View(products);



        }
    }
}
