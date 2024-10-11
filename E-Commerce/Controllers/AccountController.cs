using E_Commerce.Models;
using E_Commerce.viewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class AccountController : Controller
    {

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

                return RedirectToAction("Index" , "Home");


            }
            return View(model);
        }
    }
}
