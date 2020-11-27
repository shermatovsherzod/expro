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
    public class ExpertProfileContactsController : BaseController
    {      
        private readonly UserManager<ApplicationUser> _userManager;

        public ExpertProfileContactsController(
             UserManager<ApplicationUser> userManager
             )
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);
            var userContactInfo = new ExpertProfileContactEditVM(user);
            ViewBag.Message = false;
            return View(userContactInfo);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ExpertProfileContactEditVM vmodel)
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
                user.PhoneNumber = vmodel.PhoneNumber;
                user.Fax = vmodel.Fax;
                user.WebSite = vmodel.WebSite;
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