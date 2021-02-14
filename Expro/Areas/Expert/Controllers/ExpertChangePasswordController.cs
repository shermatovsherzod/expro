using System;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class ExpertChangePasswordController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ExpertChangePasswordController(
             UserManager<ApplicationUser> userManager,
             SignInManager<ApplicationUser> signInManager
             )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            ViewBag.Message = false;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(PasswordChangeVM vmodel)
        {
            ViewBag.Message = false;
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                // ChangePasswordAsync changes the user password
                var result = await _userManager.ChangePasswordAsync(user, vmodel.Password, vmodel.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(vmodel);
                }

                // Upon successfully changing the password refresh sign-in cookie
                await _signInManager.RefreshSignInAsync(user);
                ViewBag.Message = true;
                return View();
            }

            // If we got this far, something failed, redisplay form
            return View(vmodel);
        }
    }
}