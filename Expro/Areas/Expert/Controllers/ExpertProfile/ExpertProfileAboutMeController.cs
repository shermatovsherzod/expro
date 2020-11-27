using System;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Expert.Controllers.ExpertProfile
{
    [Area("Expert")]
    public class ExpertProfileAboutMeController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ExpertProfileAboutMeController(
             UserManager<ApplicationUser> userManager
             )
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);
            var userAboutMeInfo = new ExpertProfileAboutMeEditVM(user);
            ViewBag.Message = false;
            return View(userAboutMeInfo);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ExpertProfileAboutMeEditVM vmodel)
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var user = await _userManager.FindByIdAsync(currentUserAccount.ID);
            ViewBag.Message = false;
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }

            if (ModelState.IsValid)
            {
                user.AboutMe = vmodel.AboutMe;
                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    ViewBag.Message = true;
                    return View(vmodel);
                }
                throw new Exception("Изменения не сохранены. Что то пошло не так!");
            }

            return View(vmodel);
        }
    }
}