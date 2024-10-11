using E_Commerce.Models;
using E_Commerce.viewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class AccountController : Controller
    {
        bool logedIn = false;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVm model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser();
                appUser.UserName = model.UserName;
                appUser.PasswordHash = model.Password;
                appUser.Email = model.Email;
                appUser.Country = model.Country;
                appUser.BillingAddress = "";
                appUser.DefaultShipingAddress = "";
                
                IdentityResult result = await userManager.CreateAsync(appUser, model.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                var claim =new List<Claim> { new Claim(ClaimTypes.Role, model.Role) };

                var claimsReuslt = await userManager.AddClaimsAsync(appUser, claim);
                if (!claimsReuslt.Succeeded)
                    return BadRequest(claimsReuslt.Errors);
                logedIn = true;
                ViewBag.status = logedIn;
                return RedirectToAction("Index" , "Home");


            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    bool isFound = await userManager.CheckPasswordAsync(user, model.Password);
                    if (isFound)
                    {

                        await signInManager.SignInAsync(user,false);

                        logedIn = true;
                        ViewBag.status = logedIn;
                        return RedirectToAction("Index", "Home");
                    }

                }
                ModelState.AddModelError("msg", "wrong username or password");
            }

            return View(model ?? new LoginVm());
        }
    }
}

