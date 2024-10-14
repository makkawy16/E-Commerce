using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.viewModels;
using Elfie.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        
        private readonly ApplicationDbContext _context;
       private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment )
        {

            _context = context;
            _webHostEnvironment = webHostEnvironment;   

        }

        public IActionResult Index()
        {
            var products = _context.products.ToList();
            return View(products);
        }
        [HttpGet]
        //this action is in form of get because it generates new page
        public IActionResult Create()
        {
            //object initializer for cateogrylist
            //ProductVM contains both (product and categorylist)
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _context.categories.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()


                }).ToList()


            };


            return View(productVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken] //Protects from Cross Side Frgery Attacks
        //we need to get info from user
        //Controller is the link between model and the view
        public IActionResult Create(ProductVM productVM, IFormFile file)
        {

            if (ModelState.IsValid)
            {

                string rootpath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {

                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(rootpath, @"NImages\Products");
                    var ext = Path.GetExtension(file.FileName);

                    using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                    {
                        file.CopyTo(filestream);


                    }
                    productVM.Product.Img = @"NImages\Products\" + filename + ext;

                }

                _context.products.Add(productVM.Product);
                _context.SaveChanges();
                TempData["Create"] = "Data Has Been Created Succesfully";

                return RedirectToAction("Index");
            }
           


                return View(productVM.Product); //what was written remains
            
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null | Id == 0)
            {
                NotFound();
            }


            Product? ProductIndb = _context.products.Find(Id);
            return View(ProductIndb);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product Product)
        {
            if (ModelState.IsValid)
            {
                _context.products.Update(Product);
                _context.SaveChanges();
                TempData["Update"] = "Data Has Been Updated Succesfully";

                return RedirectToAction("Index");
            }

            return View(Product);

        }
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null | Id == 0)
            {
                return NotFound();
            }


            Product? ProductIndb = _context.products.Find(Id);
            return View(ProductIndb);

        }


        [HttpPost]
        public IActionResult DeleteProduct(int? Id) //Different name because same parameters 
        {


            // Find the Product in the database by Id
            var ProductIndb = _context.products.Find(Id);

            // Check if the Product was not found
            if (ProductIndb == null)
            {
                return NotFound(); // Return NotFound if Product is not in the database
            }

            // Remove the found Product from the database
            _context.products.Remove(ProductIndb);

            // Save changes to persist the removal
            _context.SaveChanges();
            TempData["Delete"] = "Data Has Been Deleted Succesfully";

            // Redirect to the Index action after successful deletion
            return RedirectToAction("Index");
        }


    }
}
