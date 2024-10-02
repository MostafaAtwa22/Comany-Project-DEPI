using DEPI_Final_Project.Models;
using DEPI_Final_Project.ViewModels.AccountVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DEPI_Final_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController
            (UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            userManager = _userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserVM newUserVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser
                {
                    Email = newUserVM.Email,
                    UserName = newUserVM.UserName,
                    PasswordHash = newUserVM.Password,
                };

                IdentityResult result = await userManager.CreateAsync(userModel, newUserVM.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(userModel, isPersistent: false); 
                    return RedirectToAction(nameof(Login));
                }
                foreach (var item in result.Errors)
                    ModelState.AddModelError("", item.Description);
            }
            return View(newUserVM);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser? userModel = await userManager.FindByEmailAsync(userVM.Email);
                if (userModel is not null)
                {
                    bool find = await userManager.CheckPasswordAsync(userModel, userVM.Password);
                    if (find == true)
                    {
                        List<Claim> claims = new List<Claim>();
                        await signInManager.SignInWithClaimsAsync(userModel, userVM.RemeberMe, claims);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "User and Password is invalid");
            }
            return View(userVM);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync(); 
            return RedirectToAction("Login");
        }
    }
}
